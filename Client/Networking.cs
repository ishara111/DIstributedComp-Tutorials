using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

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
        private DataModel job;
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

            clients = clients.OrderBy(client => rnd.Next()).ToList(); //randomise clients list to make job taking fair
        }


        public void FindJobs()
        {
            foreach (ClientModel c in clients)
            {
                if (c.ip.Equals(ip) && c.port.Equals(port))
                {

                }
                else
                {
                    try
                    {
                        ChannelFactory<ServerInterface> foobFactory;
                        NetTcpBinding tcp = new NetTcpBinding();
                        //Set the URL and create the connection!
                        string URL = "net.tcp://" + c.ip + ":" + c.port + "/Server";
                        foobFactory = new ChannelFactory<ServerInterface>(tcp, URL);
                        connection = foobFactory.CreateChannel();


                        if (connection.HasJob() == true && connection.IsClaimed()==false)
                        {
                            window.working = true;
                            connection.ClaimJob(true);
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

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] jobHash = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(job.encoded));

                if (jobHash.SequenceEqual(job.sha))
                {
                    var decodebytes = Convert.FromBase64String(job.encoded);
                    var decoded = Encoding.UTF8.GetString(decodebytes);

                    //RUN PYTHON CODE
                    ScriptEngine engine = Python.CreateEngine();
                    ScriptScope scope = engine.CreateScope();
                    engine.Execute(decoded, scope);
                    dynamic func = scope.GetVariable("func");
                    object result = func();


                    solution = result.ToString();
                    byte[] bytes = Encoding.UTF8.GetBytes(solution);
                    var encoded = Convert.ToBase64String(bytes);

                    byte[] solutionHash = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(encoded));

                    DataModel data = new DataModel();
                    data.encoded = encoded;
                    data.sha = solutionHash;

                    connection.SetSolution(data);

                    count++;
                    window.SetJobsDone(count);
                    window.working = false;
                    connection.ClaimJob(false);
                }
                else
                {
                    connection.ClaimJob(false);
                    window.working = false;
                }
            }
        }
    }
}
