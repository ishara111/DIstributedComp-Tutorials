using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    [ServiceContract]
    public interface ServerInterface
    {
        [OperationContract]
        bool HasJob();
        [OperationContract]
        DataModel GetJob();
        [OperationContract]
        void ClaimJob(bool claim);
        [OperationContract]
        bool IsClaimed();
        [OperationContract]
        void SetSolution(DataModel data);
    }
}
