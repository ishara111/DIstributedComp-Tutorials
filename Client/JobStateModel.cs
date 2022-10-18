using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class JobStateModel
    {
        public int Id { get; set; }
        public bool state { get; set; }
        public int clientId { get; set; }
    }
}
