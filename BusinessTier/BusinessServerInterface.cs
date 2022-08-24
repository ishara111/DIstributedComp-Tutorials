using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTier
{
    [ServiceContract]
    public interface BusinessServerInterface
    {
        //Each of these are service function contracts. They need to be tagged as OperationContracts.
        [OperationContract]
        int GetNumEntries();

        [OperationContract]
        void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out string image);

        [OperationContract]
        void GetValuesForSearch(string searchText, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out string image);
    }
}
