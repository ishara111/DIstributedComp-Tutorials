/* Name: Ishara Gomes
 * ID: 20534521
 * 
 * Description: Interface for authenticator
 */
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
        string Register(string name, string password);

        [OperationContract]
        int Login(string name, string password);

        [OperationContract]
        string Validate(int token);

    }
}
