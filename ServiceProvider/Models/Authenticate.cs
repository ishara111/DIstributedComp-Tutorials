/* Name: Ishara Gomes
 * ID: 20534521
 * 
 * Description: used to connect to authenticator
 */
using Authenticator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web;

namespace ServiceProvider.Models
{
    public class Authenticate
    {
        public AuthenticateInterface authenticate;

        public Authenticate()
        {
            ChannelFactory<AuthenticateInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            //Set the URL and create the connection!
            string URL = "net.tcp://localhost:8100/AuthenticationService";
            foobFactory = new ChannelFactory<AuthenticateInterface>(tcp, URL);
            authenticate = foobFactory.CreateChannel();
        }

    }
}