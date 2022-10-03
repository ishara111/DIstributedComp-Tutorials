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
    public class DeleteController : ApiController
    {

        // DELETE: api/Delete/5
        public IHttpActionResult Delete(int id)
        {
            RestClient restClient = new RestClient("https://localhost:44344/");

            RestRequest restRequest = new RestRequest("api/accinfo/{id}", Method.Delete);
            //restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
            restRequest.AddUrlSegment("id", id);
            RestResponse restResponse = restClient.Execute(restRequest);

            return Ok("Deleted");
        }
    }
}
