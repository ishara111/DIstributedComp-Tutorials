using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Home";
            RestClient restClient = new RestClient("https://localhost:44366/");
            RestRequest restRequest = new RestRequest("api/getacc", Method.Get);
            // restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
            RestResponse restResponse = restClient.Execute(restRequest);
            List<Accinfo> accinfoList = JsonConvert.DeserializeObject<List<Accinfo>>(restResponse.Content);




            return View(accinfoList);
        }
    }
}
