﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessTierWebAPI.Models
{
    public class GenerateData
    {
        private static Random rnd = new Random();
        private string GetFirstname()
        {
            string firstName;
            string[] fNames = { "John", "Jake", "Vincent", "Troy", "Paul", "Tom", "Jerry", "Harry", "Simon" };
            //Random rnd = new Random();
            int index = rnd.Next(fNames.Length);
            firstName = fNames[index];
            return firstName;
        }

        private string GetLastname()
        {
            string lastName;
            string[] lNames = { "Holand", "Wesley", "Smith", "Johnson", "Williams", "brown", "Miller" };
            //Random rnd = new Random();
            int index = rnd.Next(lNames.Length);
            lastName = lNames[index];
            return lastName;
        }

        private int GetPIN()
        {
            int pin;
            //Random rnd = new Random();
            pin = rnd.Next(1000, 9999);
            return pin;
        }

        private int GetAcctNo()
        {
            int acctNo;
            //Random rnd = new Random();
            acctNo = rnd.Next(100000000, 999999999);
            return acctNo;
        }

        private int GetBalance()
        {
            int balance;
            //Random rnd = new Random();
            balance = rnd.Next(0, 2000);
            return balance;
        }

        private string GetImage()
        {
            string image;
            int id;
            id = rnd.Next(151, 300);
            image = "https://picsum.photos/id/" + id.ToString() + "/200/200";
            return image;
        }

        //public Accinfo GetNextAccount()
        //{
        //    Accinfo data = new Accinfo();
        //    data.pin = GetPIN();
        //    data.accno = GetAcctNo();
        //    data.fname = GetFirstname();
        //    data.lname = GetLastname();
        //    data.balance = GetBalance();
        //    data.imageurl = GetImage();

        //    return data;
        //}

        public void GetNextAccount(out int pin, out int acctNo, out string firstName, out string lastName, out int balance, out string image)
        {
            pin = GetPIN();
            acctNo = GetAcctNo();
            firstName = GetFirstname();
            lastName = GetLastname();
            balance = GetBalance();
            image = GetImage();
        }
    }
}