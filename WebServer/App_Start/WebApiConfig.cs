using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Web.Http;
using WebServer.Models;

namespace WebServer
{
    public static class WebApiConfig
    {
        //private static ServerInterface connection;
        //private static Thread remThread;
        //private static DatabaseEntities clientdb = new DatabaseEntities();
        //private static DatabaseEntities2 jobdb = new DatabaseEntities2();
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //remThread = new Thread(RemoveDeadClients);
            //remThread.Start();
        }

        //private static void RemoveDeadClients()
        //{
        //    while (true)
        //    {
        //        var clients = new List<Client>(clientdb.Clients);
        //        var jobs = new List<Jobstate>(jobdb.Jobstates);
        //        var jobIds = new List<int>();

        //        foreach (var c in clients)
        //        {
        //            try
        //            {
        //                ChannelFactory<ServerInterface> foobFactory;
        //                NetTcpBinding tcp = new NetTcpBinding();
        //                //Set the URL and create the connection!
        //                string URL = "net.tcp://" + c.ip + ":" + c.port + "/Server";
        //                foobFactory = new ChannelFactory<ServerInterface>(tcp, URL);
        //                connection = foobFactory.CreateChannel();
        //            }
        //            catch (Exception)
        //            {
        //                foreach (var j in jobs)
        //                {
        //                    if (j.clientId.Equals(c.Id))
        //                    {
        //                        jobIds.Add(j.Id);
        //                    }
        //                }

        //                foreach (var id in jobIds)
        //                {
        //                    Jobstate jobstate = jobdb.Jobstates.Find(id);
        //                    if(jobstate != null)
        //                    {
        //                        jobdb.Jobstates.Remove(jobstate);
        //                        jobdb.SaveChanges();
        //                    }

        //                }

        //                Client client = clientdb.Clients.Find(c.Id);
        //                clientdb.Clients.Remove(client);
        //                clientdb.SaveChanges();
        //            }
        //        }
        //    }


        //}
    }
}
