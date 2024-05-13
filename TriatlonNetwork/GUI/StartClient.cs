using GUI.GUI;
using Networking.protocol;
using Service.Service;

namespace GUI;

public class StartClient
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
            
           
        //IChatServer server=new ChatServerMock();          
        IService server = new ServicesProxy("127.0.0.1", 55555);
        LoginForm loginForm = new LoginForm(server);
        Application.Run(loginForm);
    }
}
