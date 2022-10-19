using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, UseSynchronizationContext = false)]
    internal class ServerImpl : ServerInterface
    {
        private Server server;
        private bool claimed;

        public ServerImpl(Server server)
        {
            this.server = server;
            this.claimed = false;
        }

        public void ClaimJob(bool claim)
        {
            if (claim==true)
            {
                this.claimed = true;
            }
            else
            {
                this.claimed=false;
            }
        }
        public bool IsClaimed()
        {
            return this.claimed;
        }

        public DataModel GetJob()
        {
            byte[] bytes = Encoding.UTF8.GetBytes(this.server.GetJob());
            var encodedJob = Convert.ToBase64String(bytes);

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] jobHash = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(encodedJob));

                DataModel job = new DataModel();
                job.encoded = encodedJob;
                job.sha = jobHash;

                return job;
            }
        }

        public bool HasJob()
        {
            if (server.GetJob() != "")
            {
                return true;
            }
            return false;
        }
        public void SetSolution(DataModel data)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] solutionHash = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data.encoded));

                if (solutionHash.SequenceEqual(data.sha))
                {
                    var bytes = Convert.FromBase64String(data.encoded);
                    var decoded = Encoding.UTF8.GetString(bytes);

                    this.server.SetSolution(decoded);
                    this.server.SetJob("");
                }
            }
        }
    }
}
