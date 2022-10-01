﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessTierWebAPI.Models
{
    internal class DataStruct
    {
        public uint acctNo;
        public uint pin;
        public int balance;
        public string firstName;
        public string lastName;
        public object image;
        public DataStruct()
        {
            acctNo = 0;
            pin = 0;
            balance = 0;
            firstName = "";
            lastName = "";
            image = "";
        }
    }
}