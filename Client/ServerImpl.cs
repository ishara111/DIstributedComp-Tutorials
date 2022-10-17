﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, UseSynchronizationContext = false)]
    internal class ServerImpl : ServerInterface
    {
        private Server server;

        public ServerImpl(Server server)
        {
            this.server = server;
        }

        public string GetJob()
        {
            return this.server.GetJob();
        }

        public bool HasJob()
        {
            if (server.GetJob() != "")
            {
                return true;
            }
            return false;
        }
        public void SetSolution(object solution)
        {
            this.server.SetSolution(solution);
        }
    }
}
