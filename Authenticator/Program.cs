using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Authenticator
{
    internal class Program
    {
        static Authenticate authenticate;
        static Thread cleartokens;
        static void Main(string[] args)
        {
            authenticate = new Authenticate();

            Console.WriteLine("Authentication Server");

            Thread connection = new Thread(Connect);
            Thread clearMenu = new Thread(ClearMenu);
            cleartokens = new Thread(Clear);
            connection.Start();
            Thread.Sleep(1000);
            clearMenu.Start();

            authenticate.time = 180;

            cleartokens.Start();

        }

        private static void Connect()
        {
            ServiceHost host;
            NetTcpBinding tcp = new NetTcpBinding();
            host = new ServiceHost(typeof(Authenticate));
            host.AddServiceEndpoint(typeof(AuthenticateInterface), tcp, "net.tcp://0.0.0.0:8100/AuthenticationService");
            host.Open();
            Console.WriteLine("Server Online");
            host.Close();
        }

        private static void ClearMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Default time Interval to delete tokens: 3 mins");
            while (true)
            {
                int res;
                Console.WriteLine();
                Console.WriteLine("enter time interval to delete tokens in seconds");
                string time = Console.ReadLine();
                if (int.TryParse(time,out res))
                {
                    authenticate.time = int.Parse(time);
                }
                else
                {
                    Console.WriteLine("Input must be integer");
                }
            }
        }

        private static void Clear()
        {
            authenticate.ClearTokens();
        }
    }
}
