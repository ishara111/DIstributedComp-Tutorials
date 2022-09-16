using RegistryClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePublishingApp
{
    internal class MenuMethods
    {
        private ServiceMethods serviceMethods;
        public MenuMethods(ServiceMethods serviceMethods)
        {
            this.serviceMethods = serviceMethods;
        }

        public void Register()
        {
            Console.Write("Enter UserName: ");
            string name = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();
            Console.WriteLine(serviceMethods.Register(name, password));
        }

        public int Login()
        {
            Console.Write("Enter UserName: ");
            string name = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();
            int token = serviceMethods.Login(name, password);
            if (token == 0)
            {
                Console.WriteLine("Incorrect Username Or Password");
            }
            else
            {
                Console.WriteLine("Logged in Successfully");
            }
            return token;
        }

        public object Publish(Service service)
        {
            return null;
        }

        public void UnPublish()
        {
            Console.Write("Enter endpoint: ");
            string endpoint = Console.ReadLine();
            object res = serviceMethods.UnPublish(endpoint);
            Console.WriteLine(res);
        }



        //TODO
        //  fix unpublish api to show removed service
        //  fix register to check if user only exists noth both user and pass
        //  deserialize return obj or deserialise error obj depending on data type

    }
}
