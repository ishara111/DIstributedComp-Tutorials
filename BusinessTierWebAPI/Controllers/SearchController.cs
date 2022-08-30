using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using APIClasses;
using BusinessTierWebAPI.Models;

namespace BusinessTierWebAPI.Controllers
{
    public class SearchController : ApiController
    {
        DataServerConnection ds = new DataServerConnection();

        public DataIntermed Post([FromBody] SearchData searchData)
        {
            DataIntermed data = new DataIntermed();
            ds.FindValuesForSearch(searchData.searchStr, out data.acct, out data.pin, out data.bal, out data.fname, out data.lname, out data.image);
            return data;
        }
    }
}
