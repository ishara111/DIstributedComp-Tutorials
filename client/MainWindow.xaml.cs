using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
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
using RemotingServer;

namespace client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ServerInterface foob;
        public MainWindow()
        {
            InitializeComponent();
            ChannelFactory<RemotingServer.ServerInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            //Set the URL and create the connection!
            string URL = "net.tcp://localhost:8100/DataService";
            foobFactory = new ChannelFactory<ServerInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();
            //Also, tell me how many entries are in the DB.
            indexBox.Text = foob.GetNumEntries().ToString();
        }

        private void sButton_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            int res = 0;
            if (!int.TryParse(indexBox.Text, out res))
            {
                indexBox.Text = "incorrect";
            }
            else
            {
                index = Int32.Parse(indexBox.Text);

                //Console.WriteLine(int.TryParse(index.ToString(), out res));
                if ((index > 0) && (index <= foob.GetNumEntries()))
                {
                    string fName = "", lName = "";
                    int bal = 0;
                    uint acct = 0, pin = 0;
                    //On click, Get the index....
                    //index = Int32.Parse(indexBox.Text);
                    //Then, run our RPC function, using the out mode parameters...
                    foob.GetValuesForEntry(index, out acct, out pin, out bal, out fName, out lName);
                    //And now, set the values in the GUI!
                    fNameBox.Text = fName;
                    lNameBox.Text = lName;
                    balanceBox.Text = bal.ToString("C");
                    accNoBox.Text = acct.ToString();
                    pinBox.Text = pin.ToString("D4");
                }
                else
                {
                    indexBox.Text = "incorrect";
                }
            }
        }
    }
}
