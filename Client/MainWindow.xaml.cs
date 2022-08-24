using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
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
//using RemotingServer;
using BusinessTier;

namespace client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BusinessServerInterface foob;
        public MainWindow()
        {
            InitializeComponent();
            ChannelFactory<BusinessServerInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            //Set the URL and create the connection!
            string URL = "net.tcp://localhost:8200/BusinessService";
            foobFactory = new ChannelFactory<BusinessServerInterface>(tcp, URL);
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
                    string fName = "", lName = "", image = "";
                    int bal = 0;
                    uint acct = 0, pin = 0;
                    //On click, Get the index....
                    //index = Int32.Parse(indexBox.Text);
                    //Then, run our RPC function, using the out mode parameters...
                    foob.GetValuesForEntry(index, out acct, out pin, out bal, out fName, out lName, out image);
                    //And now, set the values in the GUI!
                    fNameBox.Text = fName;
                    lNameBox.Text = lName;
                    balanceBox.Text = bal.ToString("C");
                    accNoBox.Text = acct.ToString();
                    pinBox.Text = pin.ToString("D4");
                    imageBox.Source = new BitmapImage(new Uri(image));
                }
                else
                {
                    indexBox.Text = "incorrect";
                }
            }
        }

        private void nameSearch_button_Click(object sender, RoutedEventArgs e)
        {
            int res;
            Regex rgx = new Regex("[^A-Za-z0-9]");
            if ((name_search.Text.Equals("")) || ((!int.TryParse(name_search.Text, out res)) && !(rgx.IsMatch(name_search.Text))))
            {
                foob.GetValuesForSearch(name_search.Text, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out string image);
                if (!fName.Equals(""))
                {
                    fNameBox.Text = fName;
                    lNameBox.Text = lName;
                    balanceBox.Text = bal.ToString("C");
                    accNoBox.Text = acctNo.ToString();
                    pinBox.Text = pin.ToString("D4");
                    imageBox.Source = new BitmapImage(new Uri(image));
                }
                else
                {
                    name_search.Text = "not found";
                }
            }
            else
            {
                name_search.Text = "incorrect";
            }
        }
    }
}