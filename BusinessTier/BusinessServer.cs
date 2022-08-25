using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RemotingServer;


namespace BusinessTier
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    internal class BusinessServer : BusinessServerInterface
    {
        private ServerInterface foob;
        private static uint logNumber;
        //DatabaseClass db;
        public BusinessServer()
        {
            ChannelFactory<ServerInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            //Set the URL and create the connection!
            string URL = "net.tcp://localhost:8100/DataService";
            foobFactory = new ChannelFactory<ServerInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();
            BusinessServer.logNumber = 0;


            //db = new DatabaseClass();
        }
        public int GetNumEntries() 
        {
            //return db.GetNumRecords();
            BusinessServer.logNumber++;
            Log("numberofentries");


            return foob.GetNumEntries();
        }
        public void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out string image)
        {
            //acctNo = db.GetAcctNoByIndex(index);
            //pin = db.GetPINByIndex(index);
            //bal = db.GetBalanceByIndex(index);
            //fName = db.GetFirstNameByIndex(index);
            //lName = db.GetLastNameByIndex(index);
            //image = db.GetProfileImage(index);
            BusinessServer.logNumber++;
            Log("getvaluesforentry" + index);

            foob.GetValuesForEntry(index, out acctNo, out pin, out bal, out fName, out lName, out image);
        }

        public void GetValuesForSearch(string searchText, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out string image)
        {
            BusinessServer.logNumber++;
            Log("getvaluesforsearch" + searchText);
            acctNo = 0;
            pin = 0;
            bal = 0;
            fName = "";
            lName = "";
            image = "";
            for (int i = 1; i <= foob.GetNumEntries(); i++)
            {
                uint acc = 0;
                uint pinNum = 0;
                int balance = 0;
                string firstName = "";
                string lastName = "";
                string img = "";
                foob.GetValuesForEntry(i,out acc, out pinNum, out balance, out firstName, out lastName, out img);
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void Log(string logString)
        {
            Console.WriteLine("Log String: "+logString+"  Loggable Tasks Performed: "+logNumber);
        }
    }
}
