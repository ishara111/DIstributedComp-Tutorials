using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Authenticator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Authentication Server");
            ServiceHost host;
            NetTcpBinding tcp = new NetTcpBinding();
            host = new ServiceHost(typeof(Authenticate));
            host.AddServiceEndpoint(typeof(AuthenticateInterface), tcp, "net.tcp://0.0.0.0:8100/AuthenticationService");
            host.Open();
            Console.WriteLine("Server Online");
            Console.ReadLine();
            host.Close();
        }
    }
}
