using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Authenticator
{
    internal class Authenticate : AuthenticateInterface
    {
        public int time { get; set; }
        public int Login(string name, string Password)
        {
            throw new NotImplementedException();
        }

        public string Register(string name, string Password)
        {
            throw new NotImplementedException();
        }

        public string Validate(int token)
        {
            throw new NotImplementedException();
        }

        internal void ClearTokens()
        {
            while(true)
            {
                Console.WriteLine("cleared " + time);
                Thread.Sleep(1000);
            }
        }
    }
}
