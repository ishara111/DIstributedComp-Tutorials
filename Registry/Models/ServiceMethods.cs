using Newtonsoft.Json;
using RegistryClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Registry.Models
{
    public class ServiceMethods
    {
        Authenticate auth;
        int token;
        string search,folder,endpoint;
        Service service;
        List<Service> list;
        List<Service> removeServices;
        List<Service> foundServices;

        public ServiceMethods(int token, string folder, Authenticate auth)
        {
            this.token = token;
            this.folder=folder;
            this.auth = auth;
        }
        public ServiceMethods(int token, string search,string folder, List<Service> foundServices, Authenticate auth)
        {
            this.token = token;
            this.search = search;
            this.folder = folder;
            this.foundServices = foundServices;
            this.auth = auth;
        }
        public ServiceMethods(int token, string endpoint, List<Service> removeServices,string folder, Authenticate auth)
        {
            this.token = token;
            this.endpoint = endpoint;
            this.folder = folder;
            this.removeServices = removeServices;
            this.auth = auth;
        }
        public ServiceMethods(int token, Service service,string folder ,List<Service> list,Authenticate auth)
        {
            this.token = token;
            this.service = service;
            this.folder = folder;
            this.list = list;
            this.auth=auth;
        }

        public object Publish()
        {
            if (auth.authenticate.Validate(token).Equals("Validated"))
            {
                var text = File.ReadAllText(folder + "/Services.txt");
                List<Service> allServices = JsonConvert.DeserializeObject<List<Service>>(text);
                if (allServices==null)
                {
                    list.Add(service);
                    string json = JsonConvert.SerializeObject(list, Formatting.Indented);
                    File.WriteAllText(folder + "/Services.txt", json);
                }else
                {
                    allServices.Add(service);
                    string json = JsonConvert.SerializeObject(allServices, Formatting.Indented);
                    File.WriteAllText(folder + "/Services.txt", json);
                }



                return "Successfully published";
            }
            else
            {
                Error error = new Error();
                return error;
            }
        }

        public object UnPublish()
        {
            if (auth.authenticate.Validate(token).Equals("Validated"))
            {
                bool remove = false;
                var text = File.ReadAllText(folder + "/Services.txt");
                List<Service> allServices = JsonConvert.DeserializeObject<List<Service>>(text);
                if (allServices == null)
                {
                    return "No services published";
                }
                foreach (Service s in allServices)
                {
                    if (s.APIEndpoint.ToLower().Equals(endpoint))
                    {
                        removeServices.Add(s);
                        remove = true;
                    }
                }
                foreach (Service s in removeServices)
                {
                    allServices.Remove(s);
                }
                if (remove)
                {
                    string json = JsonConvert.SerializeObject(allServices, Formatting.Indented);
                    File.WriteAllText(folder + "/Services.txt", json);
                }
                else
                {
                    return "endpoint not found";
                }

                return "Service UnPublished Successfully";
            }
            else
            {
                Error error = new Error();
                return error;
            }
        }

        public object AllServices()
        {
            if (auth.authenticate.Validate(token).Equals("Validated"))
            {
                var text = File.ReadAllText(folder + "/Services.txt");
                List<Service> list = JsonConvert.DeserializeObject<List<Service>>(text);
                if (list == null || !list.Any())
                {
                    return "No services published";
                }

                return list;
            }
            else
            {
                Error error = new Error();
                return error;
            }
        }

        public object Search()
        {
            if (auth.authenticate.Validate(token).Equals("Validated"))
            {
                bool found = false;
                var text = File.ReadAllText(folder + "/Services.txt");
                List<Service> allServices = JsonConvert.DeserializeObject<List<Service>>(text);
                if (allServices == null)
                {
                    return "No services published";
                }
                foreach (Service s in allServices)
                {
                    if (s.name.ToLower().Contains(search) || s.description.ToLower().Contains(search) || s.APIEndpoint.ToLower().Contains(search) || s.NoOfOperands.ToString().Equals(search) || s.operandType.ToLower().Contains(search))
                    {
                        foundServices.Add(s);
                        found = true;
                    }
                }
                if (found)
                {
                    return foundServices;
                }
                return "not found";
            }
            else
            {
                Error error = new Error();
                return error;
            }
        }
    }
}