/* Name: Ishara Gomes
 * ID: 20534521
 * 
 * Description: conatins all methods called in menu selection that will use service methods to make requests
 */
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

        public void Register() //register
        {
            Console.WriteLine();
            Console.Write("     Enter UserName: ");
            string name = Console.ReadLine();
            Console.WriteLine();
            Console.Write("     Enter Password: ");
            string password = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("          "+serviceMethods.Register(name, password));
        }

        public int Login() //login
        {
            Console.WriteLine();
            Console.Write("     Enter UserName: ");
            string name = Console.ReadLine();
            Console.WriteLine();
            Console.Write("     Enter Password: ");
            string password = Console.ReadLine();
            int token = serviceMethods.Login(name, password);
            if (token == 0)
            {
                Console.WriteLine();
                Console.WriteLine("          Incorrect Username Or Password");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("          Logged in Successfully");
                Console.WriteLine();
            }
            return token;
        }

        public void Publish()  //publish
        {
            int numOps = 0;
            int num;
            bool notInt = false;
            Console.WriteLine();
            Console.Write("     Enter Service Name: ");
            string name = Console.ReadLine();
            Console.WriteLine();
            Console.Write("     Enter Description: ");
            string description = Console.ReadLine();
            Console.WriteLine();
            Console.Write("     Enter API Endpoint: ");
            string endpoint = Console.ReadLine();
            Console.WriteLine();
            while (!notInt)
            {
                Console.Write("     Enter No Of Operands: ");
                string inp = Console.ReadLine();
                if (int.TryParse(inp,out num))
                {
                    numOps = num;
                    notInt = true;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("     Input Must be Integer try Again");
                }
            }
            Console.WriteLine();
            Console.Write("     Enter Operand Type: ");
            string opType = Console.ReadLine();
            Service service = new Service();
            service.name = name;
            service.description = description;
            service.APIEndpoint = endpoint;
            service.NoOfOperands = numOps;
            service.operandType = opType;
            object res = serviceMethods.Publish(service);
            if (res.Equals("{\"status\":\"Denied\",\"reason\":\"Authentication Error\"}"))
            {
                Console.WriteLine();
                Console.WriteLine("          Access Denied Pls Login First");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("          Service Published Successfully");
                Console.WriteLine();
            }
        }

        public void UnPublish() //unpublsih
        {
            Console.WriteLine();
            Console.Write("     Enter API Endpoint: ");
            string endpoint = Console.ReadLine();
            object res = serviceMethods.UnPublish(endpoint);
            if (res.Equals("{\"status\":\"Denied\",\"reason\":\"Authentication Error\"}"))
            {
                Console.WriteLine();
                Console.WriteLine("          Access Denied Pls Login First");
                Console.WriteLine();
            }
            else if(res.Equals("\"No services published\""))
            {
                Console.WriteLine();
                Console.WriteLine("          No Services Published (publish first)");
                Console.WriteLine();
            }
            else if (res.Equals("\"endpoint not found\""))
            {
                Console.WriteLine();
                Console.WriteLine("          Entered Endpoint Not Found");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("          Service UnPublished Successfully");
                Console.WriteLine();
            }
        }

    }
}
