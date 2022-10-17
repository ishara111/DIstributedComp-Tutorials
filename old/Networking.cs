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
        private List<Client> clients;
        public string ip;
        public int port;
        private ServerInterface connection;
        private Server server;
        private Random rnd;


        public Networking(Server server,string ip,int port)
        {
            this.server = server;
            this.ip = ip;
            this.port = port;
            rnd = new Random();
        }
        public void GetList()
        {
            RestRequest restRequest = new RestRequest("api/client", Method.Get);
            //restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
            RestResponse restResponse = restClient.Execute(restRequest);
            clients = JsonConvert.DeserializeObject<List<Client>>(restResponse.Content);

            clients = (List<Client>)clients.OrderBy(client => rnd.Next()); //randomise clients list to make job taking fair
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
            foreach (Client c in clients)
            {
                if (c.ip.Equals(ip) && c.port.Equals(port))
                {
                    ChannelFactory<ServerInterface> foobFactory;
                    NetTcpBinding tcp = new NetTcpBinding();
                    //Set the URL and create the connection!
                    string URL = "net.tcp://"+ip+":"+port+"/Server";
                    foobFactory = new ChannelFactory<ServerInterface>(tcp, URL);
                    connection = foobFactory.CreateChannel();

                    if(connection.HasJob()==true)
                    {

                    }
                    //now connection. check if has job if yes get job and do it and retun it
                }
            }
        }

        private Object DoJob()
        {
            return null;
        }
    }
}
