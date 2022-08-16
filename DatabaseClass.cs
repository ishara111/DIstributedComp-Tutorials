using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib
{
    public class DatabaseClass
    {
        List<DataStruct> dataStruct;
        DataStruct data;
        DatabaseGen dbGen;
        public DatabaseClass()
        {
            dataStruct = new List<DatabaseLib.DataStruct>();
            data = new DataStruct();
            dbGen = new DatabaseGen();
            addData();
        }
        private void addData()
        {
            for (int i = 0; i < 20; i++)
            {
                dbGen.GetNextAccount(out data.pin, out data.acctNo, out data.firstName, out data.lastName, out data.balance);
                dataStruct.Add(data);
            }
        }
        public uint GetAcctNoByIndex(int index)
        {
            return dataStruct[index].acctNo;
        }
        public uint GetPINByIndex(int index)
        {
            return dataStruct[index].pin;
        }
        public string GetFirstNameByIndex(int index)
        {
            return dataStruct[index].firstName;
        }
        public string GetLastNameByIndex(int index)
        {
            return dataStruct[index].lastName;
        }
        public int GetBalanceByIndex(int index)
        {
            return dataStruct[index].balance;
        }
        public int GetNumRecords()
        {
            return (int)dataStruct.Count;
        }


    }
}
