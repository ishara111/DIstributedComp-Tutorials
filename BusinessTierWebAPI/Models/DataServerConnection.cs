using DataServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Web;

namespace BusinessTierWebAPI.Models
{
    public class DataServerConnection
    {
        public ServerInterface connection;

        public DataServerConnection()
        {
            Connection();
        }
        public void Connection()
        {
            ChannelFactory<ServerInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            //Set the URL and create the connection!
            string URL = "net.tcp://localhost:8100/DataService";
            foobFactory = new ChannelFactory<ServerInterface>(tcp, URL);
            connection = foobFactory.CreateChannel();
        }
    }
}