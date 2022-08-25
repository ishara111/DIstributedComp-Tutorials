﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//using RemotingServer;
using BusinessTier;
using Client;

namespace client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public delegate void Search(string name, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out string image);
    public partial class MainWindow : Window
    {
        private Data data;
        private BusinessServerInterface foob;
        private string searchval;
        private bool notFound;
        //private uint acctNo = 0;
        //private uint pin = 0;
        //private int bal = 0;
        //private string fName = "";
        //private string lName = "";
        //private string image = "";

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
            searchval = name_search.Text;
            notFound = false;
            int res;
            Regex rgx = new Regex("[^A-Za-z0-9]");
            if (!String.IsNullOrEmpty(name_search.Text))
            {
                if ((!int.TryParse(name_search.Text, out res)) && !(rgx.IsMatch(name_search.Text)))
                {
                    name_search.IsReadOnly = true;
                    indexBox.IsReadOnly = true;
                    sButton.IsEnabled = false;
                    nameSearch_button.IsEnabled = false;
                    progress_bar.IsIndeterminate = true;
                    Search search;
                    AsyncCallback searchCallback;
                    MainWindow mw = new MainWindow();
                    search = mw.foob.GetValuesForSearch;
                    searchCallback = this.OnSearchCompletion;
                    search.BeginInvoke(searchval, out uint ac, out uint p, out int b, out string f, out string l, out string i, searchCallback, null);

                    if (notFound == true)
                    {
                        name_search.Text = "not found";
                    }
                    else
                    {
                        Update();
                    }

                    progress_bar.IsIndeterminate = false;
                    name_search.IsReadOnly = false;
                    indexBox.IsReadOnly = false;
                    sButton.IsEnabled = true;
                    nameSearch_button.IsEnabled = true;
                }
                else
                {
                    name_search.Text = "incorrect";
                }
            }
            else
            {
                name_search.Text = "incorrect";
            }


        }

        private void OnSearchCompletion(IAsyncResult asyncResult)
        {
            data = new Data();

            Search search;
            AsyncResult asyncobj = (AsyncResult)asyncResult;

            if (asyncobj.EndInvokeCalled == false)
            {
                search = (Search)asyncobj.AsyncDelegate;
                search.EndInvoke(out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out string image, asyncobj);

                if (!fName.Equals(""))
                {
                    data.fname = fName;
                    data.lname = lName;
                    data.bal = bal.ToString("C");
                    data.acc = acctNo.ToString();
                    data.pin = pin.ToString("D4");
                    data.img = image;
                }
                else
                {
                    notFound = true;
                }


            }
            asyncobj.AsyncWaitHandle.Close();
        }

        private void Update()
        {
            fNameBox.Text = data.fname;
            lNameBox.Text = data.lname;
            balanceBox.Text = data.bal;
            accNoBox.Text = data.acc;
            pinBox.Text = data.pin;
            imageBox.Source = new BitmapImage(new Uri(data.img));
        }

    }
}
