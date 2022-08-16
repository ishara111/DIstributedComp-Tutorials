using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib
{
    internal class DatabaseGen
    {
        private string GetFirstname()
        {
            string firstName;
            string[] fNames = { "John", "Jake", "Vincent", "Troy", "Paul", "Tom", "Jerry", "Harry", "Simon" };
            Random rnd = new Random();
            int index = rnd.Next(fNames.Length);
            firstName = fNames[index];
            return firstName;
        }

        private string GetLastname()
        {
            string lastName;
            string[] lNames = { "Holand", "Wesley", "Smith", "Johnson", "Williams", "brown", "Miller" };
            Random rnd = new Random();
            int index = rnd.Next(lNames.Length);
            lastName = lNames[index];
            return lastName;
        }

        private uint GetPIN()
        {
            uint pin;
            Random rnd = new Random();
            pin = (uint)rnd.Next(1000, 9999);
            return pin;
        }

        private uint GetAcctNo()
        {
            uint acctNo;
            Random rnd = new Random();
            acctNo = (uint)rnd.Next(100000000, 999999999);
            return acctNo;
        }

        private int GetBalance()
        {
            int balance;
            Random rnd = new Random();
            balance = rnd.Next(0, 2000);
            return balance;
        }

        public void GetNextAccount(out uint pin, out uint acctNo, out string firstName, out string lastName, out int balance)
        {
            pin = GetPIN();
            acctNo = GetAcctNo();
            firstName = GetFirstname();
            lastName = GetLastname();
            balance = GetBalance();
        }
    }
}
