using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace BusinessTierWebAPI.Models
{
    public class SearchImpl
    {
        static DataServerConnection ds;
        public SearchImpl()
        {
            ds = new DataServerConnection();
        }
        public void FindValuesForSearch(string searchText, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out string image)
        {
            acctNo = 0;
            pin = 0;
            bal = 0;
            fName = "";
            lName = "";
            image = "";
            for (int i = 1; i <= ds.connection.GetNumEntries(); i++)
            {
                uint acc = 0;
                uint pinNum = 0;
                int balance = 0;
                string firstName = "";
                string lastName = "";
                string img = "";
                ds.connection.GetValuesForEntry(i, out acc, out pinNum, out balance, out firstName, out lastName, out img);
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