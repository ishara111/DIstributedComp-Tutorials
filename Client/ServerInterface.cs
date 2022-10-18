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
        string GetJob();
        [OperationContract]
        void EndJob();
        [OperationContract]
        void SetSolution(string solution);
    }
}
