using APIClasses;
using BusinessTierWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BusinessTierWebAPI.Controllers
{
    public class GetValuesController : ApiController
    {
        DataServerConnection ds = new DataServerConnection();
        DataIntermed dataIntermed = new DataIntermed();
        public DataIntermed Get(int id)
        {
            ds.connection.GetValuesForEntry(id,out dataIntermed.acct, out dataIntermed.pin, out dataIntermed.bal, out dataIntermed.fname, out dataIntermed.lname, out dataIntermed.image);
            return dataIntermed;
        }
    }
}
