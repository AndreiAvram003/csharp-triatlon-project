using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using Model.Domain;
using Networking.DTO;
using Service.Service;

namespace Networking.protocol
{

    public class ServicesProxy : IService
    {
        private string host;
        private int port;

        private IRefereeObserver client;
        private BinaryReader input;
        private BinaryWriter output;
        private TcpClient connection;

        private BlockingCollection<Response> responses;
        private volatile bool finished;

        public ServicesProxy(string host, int port)
        {
            this.host = host;
            this.port = port;
            responses = new BlockingCollection<Response>();
        }

        private void CloseConnection()
        {
            finished = true;
            try
            {
                input.Close();
                output.Close();
                connection.Close();
                client = null;
            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void SendRequest(Request request)
        {
            if (output == null)
            {
                InitializeConnection(); // Inițializați conexiunea dacă output este null
            }

            try
            {
                // Serializarea și trimiterea cererii
                byte[] requestData = Serialize(request);
                output.Write(requestData);
                output.Flush();
            }
            catch (IOException e)
            {
                throw new Exception("Error sending object " + e);
            }
        }

        private Response ReadResponse()
        {
            Response response = null;
            try
            {
                // Deserializarea răspunsului
                byte[] responseData = new byte[4096]; // Ajustați dimensiunea bufferului după necesitate
                int bytesRead = input.Read(responseData, 0, responseData.Length);
                response = Deserialize(responseData);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return response;
        }

        private void InitializeConnection()
        {
            try
            {
                connection = new TcpClient(host, port);
                output = new BinaryWriter(connection.GetStream());
                input = new BinaryReader(connection.GetStream());
                finished = false;
                StartReader();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void StartReader()
        {
            Thread thread = new Thread(new ThreadStart(ReadResponseThread));
            thread.Start();
        }

        private void ReadResponseThread()
        {
            while (!finished)
            {
                try
                {
                    // Așteptați și citiți răspunsul
                    Response response = ReadResponse();
                    // Adăugați răspunsul la coadă
                    responses.Add(response);
                }
                catch (SocketException e)
                {
                    finished = true;
                }
            }
        }

        public Referee Login(string username, string password, IRefereeObserver client)
        {
            InitializeConnection();
            RefereeDTO dto = new RefereeDTO(12, password, username, null);
            this.client = client;
            Console.WriteLine(client);
            Request request = new Request.Builder().Type(RequestType.LOGIN).Data(dto).Build();
            SendRequest(request);
            Response response = ReadResponse();

            if (response.Type == ResponseType.OK)
            {
                RefereeDTO refereeDTO = (RefereeDTO)response.Data;
                TrialDTO trialDTO = refereeDTO.TrialDTO;
                Trial trial = DTOUtils.GetFromDTO(trialDTO);
                Referee referee = new Referee(12, refereeDTO.Password, refereeDTO.Name, trial);
                referee.id = refereeDTO.Id;
                this.client = client;
                Console.WriteLine(client);
                return referee;
            }
            else
            {
                CloseConnection();
                return null;
            }
        }

        public void Logout(string username, string password, IRefereeObserver client)
        {
            RefereeDTO refereeDTO = new RefereeDTO(12, password, username, null);
            Request request = new Request.Builder().Type(RequestType.LOGOUT).Data(refereeDTO).Build();
            SendRequest(request);
            Response response = ReadResponse();
            if (response.Type == ResponseType.ERROR)
            {
                throw new Exception(response.Data.ToString());
            }

            CloseConnection();
        }

        public List<Participant> GetParticipants(Referee referee)
        {
            RefereeDTO refereeDTO = DTOUtils.GetDTO(referee);
            Request request = new Request.Builder().Type(RequestType.GET_PARTICIPANTS).Data(refereeDTO).Build();
            SendRequest(request);
            Response response = ReadResponse();
            this.client = client;

            if (response.Type == ResponseType.PARTICIPANTS)
            {
                List<ParticipantDTO> participantDTOS = (List<ParticipantDTO>)response.Data;
                return DTOUtils.GetFromDTO(participantDTOS);
            }

            throw new Exception("There was an error");
        }

        public Result AddResult(Participant participant, Trial trial, int points)
        {
            Result result = new Result(participant, trial, points);
            ResultDTO resultDTO = DTOUtils.GetDTO(result);
            Request request = new Request.Builder().Type(RequestType.ADD_RESULT).Data(resultDTO).Build();
            SendRequest(request);
            Response response = ReadResponse();
            if (response.Type == ResponseType.ERROR)
            {
                return null;
            }

            return result;
        }

        public int GetTotalPointsAtTrial(Participant participant, Trial trial)
        {
            ParticipantDTO participantDTO = DTOUtils.GetDTO(participant);
            TrialDTO trialDTO = DTOUtils.GetDTO(trial);
            ParticipantTrialData participantTrialData = new ParticipantTrialData(participantDTO, trialDTO);
            Request request = new Request.Builder().Type(RequestType.POINTS_AT_TRIAL).Data(participantTrialData)
                .Build();
            SendRequest(request);
            this.client = client;
            Response response = ReadResponse();
            if (response.Type == ResponseType.POINTS_AT_TRIAL)
            {
                return (int)response.Data;
            }

            throw new Exception("There was an error");
        }

        public List<Participant> GetParticipantsAtTrial(Trial trial)
        {
            TrialDTO trialDTO = DTOUtils.GetDTO(trial);
            Request request = new Request.Builder().Type(RequestType.FILTERED_PARTICIPANTS).Data(trialDTO).Build();
            SendRequest(request);
            Response response = ReadResponse();

            if (response.Type == ResponseType.FILTERED_PARTICIPANTS)
            {
                List<ParticipantDTO> participantDTOS = (List<ParticipantDTO>)response.Data;
                return DTOUtils.GetFromDTO(participantDTOS);
            }

            throw new Exception("There was an error");
        }

        private byte[] Serialize(object obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    // Serializare obiect în fluxul de memorie
                    // Aici puteți utiliza o librărie de serializare, cum ar fi JSON.NET, dacă preferați
                }

                return stream.ToArray();
            }
        }

        private Response Deserialize(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    // Deserializare flux de memorie în obiect Response
                    // Aici puteți utiliza o librărie de serializare/deserializare, cum ar fi JSON.NET, dacă preferați
                }
            }

            return null;
        }

        private void HandleUpdate(Response response)
        {
            try
            {
                Console.WriteLine("apelez update controller");
                Console.WriteLine(client);
                client.Update();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private bool IsUpdate(Response response)
        {
            return response.Type == ResponseType.RESULT_ADDED;
        }
    }

}