﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Authenticator
{
    internal class Authenticate : AuthenticateInterface
    {
        const string UserFile = "Users.txt";
        const string TokenFile = "Tokens.txt";
        public int time { get; set; }
        private static Random rnd = new Random();

        public string Register(string name, string password)
        {
            FileExists(UserFile);

            if (!ReadUsers(name,password))
            {
                WriteUsers(name,password);

                return "Successfully Registered";
            }
            else
            {
                return "User already exists";
            }

        }
        public int Login(string name, string password)
        {
            FileExists(UserFile);
            if (ReadUsers(name,password))
            {
                int token = GenerateToken();
                FileExists(TokenFile);
                if (!ReadTokens(token))
                {
                    WriteTokens(token);
                }
                else
                {
                    token = GenerateToken();
                    while (ReadTokens(token))
                    {
                        token = GenerateToken();
                    }
                    WriteTokens(token);
                }
                return token;
            }
            else
            {
                return 0;
            }
        }

        public string Validate(int token)
        {
            if (ReadTokens(token))
            {
                return "Validated";
            }
            else
            {
                return "Not Validated";
            }
        }

        internal void ClearTokens()
        {
            while (true)
            {
                Thread.Sleep(time * 60000);

                File.WriteAllText(TokenFile, string.Empty);

                Console.WriteLine("cleared token file after " + time + " mins");
            }
        }

        private void FileExists(string filename)
        {
            if (!File.Exists(filename))
            {
                using (StreamWriter w = File.CreateText(filename)) { }
            }
        }

        private int GenerateToken()
        {
            int token;
            token = rnd.Next(100000, 999999);
            return token;
        }

        private void WriteUsers(string name, string password)
        {
            using (StreamWriter w = new StreamWriter(UserFile, true))
            {
                w.WriteLine(name + "," + password);
            }
        }

        private void WriteTokens(int token)
        {
            using (StreamWriter w = new StreamWriter(TokenFile, true))
            {
                w.WriteLine(token);
            }
        }

        private bool ReadUsers(string name, string password)
        {
            bool found = false;
            var f = File.ReadLines(UserFile);
            foreach (var l in f)
            {
                string[] content = l.Split(',');
                if (name.Equals(content[0]) && password.Equals(content[1]))
                {
                    found = true;
                }
            }
            return found;
        }

        private bool ReadTokens(int token)
        {
            bool found = false;
            var f = File.ReadLines(TokenFile);
            foreach (var l in f)
            {
                if (token.Equals(int.Parse(l)))
                {
                    found = true;
                }
            }
            return found;
        }
    }
}
