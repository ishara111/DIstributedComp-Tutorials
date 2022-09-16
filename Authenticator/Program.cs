﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Authenticator
{
    internal class Program
    {
        static Authenticate authenticate;
        static Thread connection,cleartokens;
        static void Main(string[] args)
        {
            authenticate = new Authenticate();
            authenticate.InitTokenFile();

            Console.WriteLine("Authentication Server");

            connection = new Thread(Connect);
            Thread clearMenu = new Thread(ClearMenu);
            
            connection.Start();
            Thread.Sleep(1500);
            clearMenu.Start();

            authenticate.time = 3;
        }

        private static void Connect()
        {
            ServiceHost host;
            NetTcpBinding tcp = new NetTcpBinding();
            host = new ServiceHost(typeof(Authenticate));
            host.AddServiceEndpoint(typeof(AuthenticateInterface), tcp, "net.tcp://0.0.0.0:8100/AuthenticationService");
            host.Open();
            Console.WriteLine("Server Online");
            connection.Join();
            host.Close();
        }

        private static void ClearMenu()
        {
            cleartokens = new Thread(Clear);
            cleartokens.Start();
            Console.WriteLine();
            Console.WriteLine("Default time Interval to delete tokens: 3 mins");
            while (true)
            {
                //int res=0;
                double res;
                Console.WriteLine();
                Console.WriteLine("enter time interval to delete tokens in mins");
                string time = Console.ReadLine();
                if (double.TryParse(time,out res))
                {
                    Console.WriteLine();
                    Console.WriteLine("Time Set To: " + time + " mins / " + (res * 60) + " secs");

                    authenticate.time = double.Parse(time);

                    cleartokens.Abort();
                    cleartokens = new Thread(Clear);
                    cleartokens.Start();
                }
                else
                {
                    Console.WriteLine("Input must be integer");
                }
            }
        }

        private static void Clear()
        {
            authenticate.ClearTokens();
        }
    }
}
