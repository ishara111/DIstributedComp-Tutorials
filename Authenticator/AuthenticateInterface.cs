using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Authenticator
{
    [ServiceContract]
    public interface AuthenticateInterface
    {
        [OperationContract]
        string Register(string name, string Password);

        [OperationContract]
        int Login(string name, string Password);

        [OperationContract]
        string Validate(int token);

    }
}
