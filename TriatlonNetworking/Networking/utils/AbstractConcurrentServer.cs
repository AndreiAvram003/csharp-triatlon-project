using System;
using System.Net.Sockets;
using System.Threading;
namespace Networking.utils;

        public abstract class AbstractConcurrentServer : AbstractServer
        {
            public AbstractConcurrentServer(int port) : base(port)
            {
                Console.WriteLine("Concurrent server started");
            }

            protected override void ProcessRequest(TcpClient client) // Modificați TcpClient în loc de Socket
            {
                Thread thread = CreateWorker(client.Client); // Folosiți client.Client pentru a obține obiectul Socket
                thread.Start();
            }

            protected abstract Thread CreateWorker(Socket client);
        }
    

