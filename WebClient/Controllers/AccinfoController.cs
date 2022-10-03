using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class AccinfoController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Accinfo";
            return View();
        }

        [HttpPost]
        public IActionResult generate()
        {
            RestClient restClient = new RestClient("https://localhost:44366/");
            RestRequest restRequest = new RestRequest("api/generate", Method.Post);
            //restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
            RestResponse restResponse = restClient.Execute(restRequest);
            return Ok(restResponse.Content);
        }

        [HttpPost]
        public IActionResult insert([FromBody] Accinfo data)
        {
            RestClient restClient = new RestClient("https://localhost:44366/");
            RestRequest restRequest = new RestRequest("api/insert", Method.Post);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
            RestResponse restResponse = restClient.Execute(restRequest);
            if (restResponse != null)
            {
                return Ok(restResponse);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult searchid(int id)
        {
            RestClient restClient = new RestClient("https://localhost:44366/");
            RestRequest restRequest = new RestRequest("api/search/{id}", Method.Get);
            //restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
            restRequest.AddUrlSegment("id", id);
            RestResponse restResponse = restClient.Execute(restRequest);
            return Ok(restResponse.Content);
        }

        [HttpGet]
        public IActionResult searchname(string name)
        {
            RestClient restClient = new RestClient("https://localhost:44366/");
            RestRequest request = new RestRequest("api/search?name=" + name);
            RestResponse resp = restClient.Get(request);
            RestResponse restResponse = restClient.Execute(request);
            return Ok(restResponse.Content);
        }

        [HttpPost]
        public IActionResult update(int id,[FromBody] Accinfo data)
        {
            RestClient restClient = new RestClient("https://localhost:44366/");
            RestRequest request = new RestRequest("api/update/" + id.ToString());
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            RestResponse restResponse = restClient.Post(request);
            if (restResponse != null)
            {
                return Ok(restResponse);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IActionResult delete(int id)
        {
            RestClient restClient = new RestClient("https://localhost:44366/");
            RestRequest restRequest = new RestRequest("api/delete/{id}", Method.Delete);
            //restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
            restRequest.AddUrlSegment("id", id);
            RestResponse restResponse = restClient.Execute(restRequest);
            if (restResponse != null)
            {
                return Ok(restResponse);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
