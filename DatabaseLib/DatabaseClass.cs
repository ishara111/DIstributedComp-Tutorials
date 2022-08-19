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
        //DataStruct data;
        //DatabaseGen dbGen;
        public DatabaseClass()
        {
            dataStruct = new List<DatabaseLib.DataStruct>();


            addData();
        }
        private void addData()
        {

            for (int i = 0; i < 20; i++)
            {
                DataStruct data = new DataStruct();
                DatabaseGen dbGen = new DatabaseGen();
                dbGen.GetNextAccount(out data.pin, out data.acctNo, out data.firstName, out data.lastName, out data.balance, out data.image);
                dataStruct.Add(data);
                //Console.WriteLine(data.image);
            }
        }

        public uint GetAcctNoByIndex(int index)
        {
            return dataStruct[index - 1].acctNo;
        }
        public uint GetPINByIndex(int index)
        {
            return dataStruct[index - 1].pin;
        }
        public string GetFirstNameByIndex(int index)
        {
            return dataStruct[index - 1].firstName;
        }
        public string GetLastNameByIndex(int index)
        {
            return dataStruct[index - 1].lastName;
        }
        public int GetBalanceByIndex(int index)
        {
            return dataStruct[index - 1].balance;
        }
        public string GetProfileImage(int index)
        {
            return dataStruct[index - 1].image;
        }
        public int GetNumRecords()
        {
            return (int)dataStruct.Count;
        }


    }
}
