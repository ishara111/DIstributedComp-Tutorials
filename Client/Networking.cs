using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Networking
    {
        private RestClient restClient = new RestClient("https://localhost:44379/");
        private List<ClientModel> clients;
        public string ip;
        public int port;
        private ServerInterface connection;
        private Server server;
        private Random rnd;
        private MainWindow window;
        private string job;
        private string solution;
        private int count;


        public Networking(MainWindow window, Server server, string ip, int port)
        {
            this.window = window;
            this.server = server;
            this.ip = ip;
            this.port = port;
            rnd = new Random();
            count = 0;
        }
        public void GetList()
        {
            RestRequest restRequest = new RestRequest("api/client", Method.Get);
            //restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
            RestResponse restResponse = restClient.Execute(restRequest);
            clients = JsonConvert.DeserializeObject<List<ClientModel>>(restResponse.Content);

            //clients = (List<ClientModel>)clients.OrderBy(client => rnd.Next()); //randomise clients list to make job taking fair
        }

        //public void SetIp(string ip)
        //{
        //    this.ip = ip;
        //}

        //public void SetPort(int port)
        //{
        //    this.port = port;
        //}

        public void FindJobs()
        {
            foreach (ClientModel c in clients)
            {
                //Console.WriteLine("ooooooooooooooooooooooooooooooooooooooooooooo");
                if (c.ip.Equals(ip) && c.port.Equals(port))
                {

                }
                else
                {
                    Console.WriteLine("HELLLLOL");
                    try
                    {
                        ChannelFactory<ServerInterface> foobFactory;
                        NetTcpBinding tcp = new NetTcpBinding();
                        //Set the URL and create the connection!
                        string URL = "net.tcp://" + c.ip + ":" + c.port + "/Server";
                        foobFactory = new ChannelFactory<ServerInterface>(tcp, URL);
                        connection = foobFactory.CreateChannel();

                        Console.WriteLine("JOB FOR" + c.port);

                        if (connection.HasJob() == true)
                        {
                            window.working = true;
                            Console.WriteLine("Inside loop" + c.port);
                            DoJob();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        private void DoJob()
        {
            job = connection.GetJob();

            //RUN OYTHON CODE
            solution = job;

            byte[] bytes = Encoding.UTF8.GetBytes(solution);
            var encoded = Convert.ToBase64String(bytes);

            connection.EndJob();
            connection.SetSolution(encoded);
            count++;
            window.SetJobsDone(count);
            window.working = false;
            Console.WriteLine("J========================DONE");
        }
    }
}
