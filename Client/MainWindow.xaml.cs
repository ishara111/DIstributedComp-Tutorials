using Microsoft.Win32;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string ip = "localhost";
        private int port;
        private int jobsDone;
        private bool working;
        private int clientID,jobID;
        private static Server server;
        private static Networking networking;
        private static Random rnd = new Random();
        private static Thread networkThread, serverThread;
        private static RestClient db;
        public MainWindow()
        {
            InitializeComponent();
            db = new RestClient("https://localhost:44379/");
            jobsDone = 0;
            working = false;
            clientID = 0;
            jobID = 0;
            port = GenPort();

            AddClientToDb();

            server = new Server(port);
            networking = new Networking(this,server, ip, port);

            ipBox.Text = "IP: " + ip;
            portBox.Text = "Port: " + port;

            networkThread = new Thread(StartNetworkThread);
            serverThread = new Thread(StartServerThread);
            serverThread.Start();
            networkThread.Start();
        }

        private void AddClientToDb()
        {
            ClientModel client = new ClientModel();
            client.ip = ip;
            client.port = port;

            RestRequest request = new RestRequest("api/client");
            request.AddJsonBody(client);
            RestResponse resp = db.Post(request);

            GetClientID();
            //MessageBox.Show(resp.Content);
        }

        private void GetClientID()
        {
            RestRequest restRequest = new RestRequest("api/client", Method.Get);
            //restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
            RestResponse restResponse = db.Execute(restRequest);
            var clients = JsonConvert.DeserializeObject<List<ClientModel>>(restResponse.Content);
            foreach (var c in clients)
            {
                if (c.ip.Equals(ip) && c.port.Equals(port))
                {
                    this.clientID = c.Id;
                }
            }
        }

        private void GetJobID()
        {
            RestRequest restRequest = new RestRequest("api/jobstate", Method.Get);
            //restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
            RestResponse restResponse = db.Execute(restRequest);
            var jobs = JsonConvert.DeserializeObject<List<JobStateModel>>(restResponse.Content);
            foreach (var j in jobs)
            {
                if(j.clientId.Equals(clientID))
                {
                    this.jobID = j.Id;
                }
            }
        }

        public void SetJobsDone(int count)
        {
            this.jobsDone = count;
        }

        private int GenPort()
        {
            return rnd.Next(6000, 9000);
        }

        private void StartServerThread()
        {
            server.StartServer(serverThread);
        }
        private void StartNetworkThread()
        {
            //networking.SetIp(ip);
            //networking.SetPort(port);
            while (true)
            {
                networking.GetList();
                networking.FindJobs();
                //Thread.Sleep(6000);
            }

        }
        private void fileBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select Python File";
            op.Filter = "All supported Types|*.txt;*.py;";
            if (op.ShowDialog() == true)
            {
                string contents = File.ReadAllText(op.FileName);
                server.SetJob(contents);

                JobStateModel jobstate = new JobStateModel();
                jobstate.state = false;
                jobstate.clientId = this.clientID;

                RestRequest request = new RestRequest("api/jobstate");
                request.AddJsonBody(jobstate);
                RestResponse resp = db.Post(request);
                GetJobID();

                MessageBox.Show("Job Added");
            }

        }

        private void jobBtn_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text != "")
            {
                server.SetJob(textBox.Text);

                JobStateModel jobstate = new JobStateModel();
                jobstate.state = false;
                jobstate.clientId = this.clientID;

                RestRequest request = new RestRequest("api/jobstate");
                request.AddJsonBody(jobstate);
                RestResponse resp = db.Post(request);
                GetJobID();

                MessageBox.Show("Job Added");
            }
            else
            {
                MessageBox.Show("job cannot be empty");
            }
        }

        private void statusBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Working On job: " + working + "\n\nJobs Completed: " + jobsDone);
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            RestRequest request = new RestRequest("api/client/" + clientID);
            RestResponse resp = db.Delete(request);

            RestRequest request1 = new RestRequest("api/jobstate/" + jobID);
            RestResponse resp1 = db.Delete(request1);

            this.Close();
        }
    }
}
