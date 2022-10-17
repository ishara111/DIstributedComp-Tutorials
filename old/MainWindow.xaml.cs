using Microsoft.Win32;
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
        private static Server server;
        private static Networking networking;
        private static Random rnd = new Random();
        public MainWindow()
        {
            InitializeComponent();
            port = GenPort();
            server = new Server(port);
            networking = new Networking(server,ip,port);

            ipBox.Text = "IP: " + ip;
            portBox.Text = "Port: " + port;
        }

        private int GenPort()
        {
            return rnd.Next(6000, 9000);
        }

        private void StartServerThread()
        {

        }
        private void StartNetworkThread()
        {
            //networking.SetIp(ip);
            //networking.SetPort(port);
            while (true)
            {
                networking.GetList();
                networking.FindJobs();
                Thread.Sleep(6000);
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

                MessageBox.Show("Job Added");
            }

        }

        private void jobBtn_Click(object sender, RoutedEventArgs e)
        {
            if(textBox.Text!="")
            {
                server.SetJob(textBox.Text);
                MessageBox.Show("Job Added");
            }
            else
            {
                MessageBox.Show("job cannot be empty");
            }
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
