using DatabaseLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DataServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, UseSynchronizationContext = false)]
    internal class DataServer : ServerInterface
    {
        DatabaseClass db;
        public DataServer()
        {
            db = new DatabaseClass();
        }
        public int GetNumEntries()
        {
            return db.GetNumRecords();
        }
        public void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out string image)
        {
            acctNo = db.GetAcctNoByIndex(index);
            pin = db.GetPINByIndex(index);
            bal = db.GetBalanceByIndex(index);
            fName = db.GetFirstNameByIndex(index);
            lName = db.GetLastNameByIndex(index);
            image = db.GetProfileImage(index);
        }

    }
}
