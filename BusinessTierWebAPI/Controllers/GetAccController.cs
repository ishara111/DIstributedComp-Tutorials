using BusinessTierWebAPI.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BusinessTierWebAPI.Controllers
{
    public class GetAccController : ApiController
    {
        // GET: api/GetAcc
        public List<Accinfo> Get()
        {
            RestClient restClient = new RestClient("https://localhost:44344/");
            RestRequest restRequest = new RestRequest("api/accinfo", Method.Get);
            // restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
            RestResponse restResponse = restClient.Execute(restRequest);
            List<Accinfo> accinfoList = JsonConvert.DeserializeObject<List<Accinfo>>(restResponse.Content);

            return accinfoList;
        }
    }
}
