using System.Net.Sockets;
using Networking.protocol;
using Service.Service;

namespace Networking.utils;

public class ProjectConcurrentServer : AbstractConcurrentServer
{
    private readonly IService projectServices;

    public ProjectConcurrentServer(int port, IService projectServices) : base(port)
    {
        this.projectServices = projectServices;
        Console.WriteLine("Created concurrent server");
    }

    protected override Thread CreateWorker(Socket client)
    {
        //ClientWorker worker = new ClientWorker(projectServices, new TcpClient(client)); // Convertim Socket-ul în TcpClient
        //return new Thread(worker.Run);
        return null;
    }

    /*public override void Stop()
    {
        base.Stop();
        Console.WriteLine("Stopping concurrent server");
    }*/
}
