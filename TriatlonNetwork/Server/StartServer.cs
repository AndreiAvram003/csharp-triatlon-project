using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Sockets;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Model.Domain;
using Networking;
using Networking.protocol;
using Repository;
using Repository.connectionUtils;
using Repository.Repository;
using Service.Service;

namespace Server
{
    internal class StartServer
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        private static TriatlonDBContext createDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TriatlonDBContext>();
            optionsBuilder.UseSqlite(ConfigurationManager.ConnectionStrings["TriatlonDB"].ConnectionString);
            return new TriatlonDBContext(optionsBuilder.Options);
        }   

        public static void Main(string[] args)
        {
            IDictionary<String, string> props = new SortedList<String, String>();
            props.Add("ConnectionString", ConfigurationManager.ConnectionStrings["TriatlonDB"].ConnectionString);
            
            TrialRepository repot = new TrialDBRepo(props);
            RefereeRepository repor = new RefereeDBRepo(props);
            ResultRepository repores = new ResultDBRepo(props);
            ParticipantRepository repop = new ParticipantDBRepoEF(createDbContext());
            IService service = new ServiceImpl(repop, repot, repores, repor);
            SerialProjectServer server = new SerialProjectServer("127.0.0.1", 55555, service);

            server.Start();
            Console.WriteLine("Server started...");
            Console.ReadLine();
        }
        public class SerialProjectServer : ConcurrentServer
        {
            private IService server;
            private ClientWorker worker;
            public SerialProjectServer(string host, int port, IService server) : base(host, port)
            {
                this.server = server;
                Console.WriteLine("SerialProjectServer...");
            }
            protected override Thread createWorker(TcpClient client)
            {
                worker = new ClientWorker(server, client);
                return new Thread(new ThreadStart(worker.run));
            }
        }
    }
}