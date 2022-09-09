using System;
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
        public int time { get; set; }

        public string Register(string name, string Password)
        {
            if (!ReadFile(name,Password))
            {
                using (StreamWriter w = new StreamWriter("Users.txt", true))
                {
                    w.WriteLine(name + "," + Password);
                }
                return "Successfully Registered";
            }
            else
            {
                return "User already exists";
            }

        }
        public int Login(string name, string password)
        {
            if (ReadFile(name,password))
            {
                return 1234;
            }
            else
            {
                return 0;
            }
        }

        public string Validate(int token)
        {
            throw new NotImplementedException();
        }

        internal void ClearTokens()
        {
/*            while(true)
            {
                Console.WriteLine("cleared " + time);
                Thread.Sleep(1000);
            }*/
        }

        private bool ReadFile(string name, string password)
        {
            bool found = false;
            var f = File.ReadLines("Users.txt");
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
    }
}
