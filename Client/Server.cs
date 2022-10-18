using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    internal class Server
    {
        private string job;
        private int port;
        private object solution;
        private MainWindow window;

        public Server(MainWindow window,int port)
        {
            this.job = "";
            this.port = port;
            this.window = window;
        }


        public void SetJob(string job)
        {
            this.job = job;
        }
        public string GetJob()
        {
            return this.job;
        }
        public void SetSolution(string solution)
        {
            var bytes = Convert.FromBase64String(solution);
            var decoded = Encoding.UTF8.GetString(bytes);
            this.window.SetSolution(decoded);
        }

        public void StartServer(Thread serverThread)
        {
            Console.WriteLine("hello welcome to the server");

            ServerImpl serverImpl = new ServerImpl(this);
            //This is the actual host service system
            ServiceHost host;
            //This represents a tcp/ip binding in the Windows network stack
            NetTcpBinding tcp = new NetTcpBinding();
            //Bind server to the implementation of DataServer
            host = new ServiceHost(serverImpl);

            var action = host.Description.Behaviors.Find<ServiceBehaviorAttribute>();
            action.InstanceContextMode = InstanceContextMode.Single;
            //Present the publicly accessible interface to the client. 0.0.0.0 tells .net to accept on any interface. :8100 means this will use port 8100. DataService is a name for theactual service, this can be any string.

            host.AddServiceEndpoint(typeof(ServerInterface), tcp, "net.tcp://0.0.0.0:" + this.port + "/Server");
            //And open the host for business!
            host.Open();
            Console.WriteLine("System Online");
            serverThread.Join();
            Console.WriteLine("OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO");
            //Don't forget to close the host after you're done!
            host.Close();
        }
    }
}
