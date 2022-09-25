/* Name: Ishara Gomes
 * ID: 20534521
 * 
 * Description: used to send requests to apis or server
 */
using Authenticator;
using RegistryClasses;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePublishingApp
{
    internal class ServiceMethods
    {
        private RestClient registry;
        private AuthenticateInterface authenticator;
        private int token;
        public ServiceMethods(AuthenticateInterface authenticator,RestClient registry, int token)
        {
            this.authenticator = authenticator;
            this.registry = registry;
            this.token = token;
        }

        public string Register(string name,string password)
        {
            return authenticator.Register(name,password);
        }
        public int Login(string name, string password)
        {
            return authenticator.Login(name, password);
        }
        public object Publish(Service service)
        {
            RestRequest request = new RestRequest("api/publish?token="+token);
            request.AddJsonBody(service);
            RestResponse resp = registry.Post(request);
            return resp.Content;
        }

        public object UnPublish(string endpoint)
        {
            RestRequest request = new RestRequest("api/unpublish?token=" + token+ "&endpoint="+endpoint);
            RestResponse resp = registry.Delete(request);

            return resp.Content;
        }
    }
}
