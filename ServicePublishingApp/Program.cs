using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Authenticator;
using System.Threading;

namespace ServicePublishingApp
{
    internal class Program
    {
        static AuthenticateInterface authenticator;
        static RestClient registry;
        static int token;
        public static void Main(string[] args)
        {
            token = 0;
            //Console.WriteLine("Service Publishing App");
            //Console.WriteLine();
            //Console.WriteLine("Register[r] Login[l] Publish[p] UnPublish[u]");
            Connect();
            ShowMenu();

        }

        public static void Connect()
        {
            string registryURL = "https://localhost:44327/";
            registry = new RestClient(registryURL);
            //RestRequest request = new RestRequest("api/allservices?token=555");
            //RestResponse numOfThings = registry.Get(request);
            //Console.WriteLine(numOfThings.Content);

            ChannelFactory<AuthenticateInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            string authURL = "net.tcp://localhost:8100/AuthenticationService";
            foobFactory = new ChannelFactory<AuthenticateInterface>(tcp, authURL);
            authenticator = foobFactory.CreateChannel();
        }

        public static void ShowMenu()
        {
            string service;
            while (true)
            {
                Console.WriteLine("Service Publishing App");
                Console.WriteLine();
                Console.WriteLine("Register[r] Login[l] Publish[p] UnPublish[u]");
                Console.WriteLine();
                ServiceMethods serviceMethods = new ServiceMethods(authenticator, registry, token);
                MenuMethods menuMethods = new MenuMethods(serviceMethods);
                Console.Write("Enter Service: ");
                service = Console.ReadLine();
                if (service.ToLower().Equals("r"))
                {
                    menuMethods.Register();
                    
                }
                else if (service.ToLower().Equals("l"))
                {
                    token = menuMethods.Login();
                }
                else if (service.ToLower().Equals("p"))
                {
                    menuMethods.Publish();
                }
                else if (service.ToLower().Equals("u"))
                {
                    menuMethods.UnPublish();
                }
                else
                {
                    Console.WriteLine("Incorrect Service Try Agian");
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine("PRESS ENTER TO CONTINUE");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
