using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistryClasses
{
    public class Service
    {
        public string name { get; set; }
        public string description { get; set; }
        public string APIEndpoint { get; set; }
        public int NoOfOperands { get; set; }
        public string operandType { get; set; }
    }
}
