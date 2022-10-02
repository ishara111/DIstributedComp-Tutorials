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
    public class InsertController : ApiController
    {

        // POST: api/Insert
        public IHttpActionResult Post([FromBody]Accinfo data)
        {
            RestClient restClient = new RestClient("https://localhost:44344/");
            RestRequest restRequest = new RestRequest("api/accinfo", Method.Post);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
            RestResponse restResponse = restClient.Execute(restRequest);

            return Ok(restResponse.Content);
        }
    }
}
