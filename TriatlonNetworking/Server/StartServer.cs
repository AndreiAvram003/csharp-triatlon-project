using System;
using System.Collections.Generic;
using System.IO;
using Repository.Repository;
using Networking.utils;
using Service.Service;

namespace Server
{
    class StartServer
    {
        private static readonly int defaultPort = 55555;

        static void Main(string[] args)
        {
            Dictionary<string, string> serverProps = new Dictionary<string, string>();
            try
            {
                // Încărcați fișierul de configurare
                using (StreamReader reader = new StreamReader("bd.config"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Separarea cheii și valorii
                        string[] parts = line.Split('=');
                        if (parts.Length == 2)
                        {
                            serverProps[parts[0].Trim()] = parts[1].Trim();
                        }
                    }
                }

                // Afisare proprietati
                foreach (var prop in serverProps)
                {
                    Console.WriteLine(prop.Key + ": " + prop.Value);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Cannot find bd.config " + e);
            }

            RefereeRepository refereeDBRepo = new RefereeDBRepo(serverProps);
            TrialRepository trialDBRepo = new TrialDBRepo(serverProps);
            ResultRepository resultDBRepo = new ResultDBRepo(serverProps);
            ParticipantRepository participantDBRepo = new ParticipantDBRepo(serverProps);

            IService projectServices = new ServiceImpl(participantDBRepo, trialDBRepo, resultDBRepo, refereeDBRepo);

            int projectServerPort = defaultPort;
            try
            {
                projectServerPort = int.Parse(serverProps["server.port"]);
            }
            catch (FormatException)
            {
                Console.WriteLine("Using default port: " + defaultPort);
            }

            Console.WriteLine("Starting server on port: " + projectServerPort);

            AbstractServer server = new ProjectConcurrentServer(projectServerPort, projectServices);

            try
            {
                server.Start();
            }
            catch (ServerException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                try
                {
                    server.Stop();
                }
                catch (ServerException e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
