using DataServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Web;

namespace BusinessTierWebAPI.Models
{
    public class DataServerConnection
    {
        public ServerInterface connection;

        public DataServerConnection()
        {
            ChannelFactory<ServerInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            //Set the URL and create the connection!
            string URL = "net.tcp://localhost:8100/DataService";
            foobFactory = new ChannelFactory<ServerInterface>(tcp, URL);
            connection = foobFactory.CreateChannel();
        }

        public void FindValuesForSearch(string searchText, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out string image)
        {
            acctNo = 0;
            pin = 0;
            bal = 0;
            fName = "";
            lName = "";
            image = "";
            for (int i = 1; i <= connection.GetNumEntries(); i++)
            {
                uint acc = 0;
                uint pinNum = 0;
                int balance = 0;
                string firstName = "";
                string lastName = "";
                string img = "";
                connection.GetValuesForEntry(i, out acc, out pinNum, out balance, out firstName, out lastName, out img);
                if (searchText.ToUpper().Equals(lastName.ToUpper()))
                {
                    acctNo = acc;
                    pin = pinNum;
                    bal = balance;
                    fName = firstName;
                    lName = lastName;
                    image = img;
                    break;
                }
            }
            Thread.Sleep(1000);
        }
    }
}