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
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username,string password)
        {
            RestClient restClient = new RestClient("https://localhost:44363/");
            RestRequest request = new RestRequest("api/login?username="+username+"&password="+password);
            //restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
            RestResponse restResponse = restClient.Post(request);
            if (restResponse.Content.Equals("logged in"))
            {
                return Ok(restResponse.Content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult ShowAllCentres()
        {
            RestClient restClient = new RestClient("https://localhost:44363/");
            RestRequest request = new RestRequest("api/centres");
            RestResponse resp = restClient.Get(request);
            RestResponse restResponse = restClient.Execute(request);
            List<Centre> centreList = JsonConvert.DeserializeObject<List<Centre>>(restResponse.Content);
            return Ok(centreList);
        }

        [HttpGet]
        public IActionResult GetCentreBookings(int centreId)
        {
            RestClient restClient = new RestClient("https://localhost:44363/");
            RestRequest request = new RestRequest("api/CentreBookings/"+centreId);
            RestResponse resp = restClient.Get(request);
            RestResponse restResponse = restClient.Execute(request);
            List<Booking> bookingList = JsonConvert.DeserializeObject<List<Booking>>(restResponse.Content);
            if (bookingList.Count()>0)
            {
                return Ok(bookingList);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult AddCentre([FromBody] Centre centre)
        {
            RestClient restClient = new RestClient("https://localhost:44363/");
            RestRequest request = new RestRequest("api/centres");
            request.AddJsonBody(JsonConvert.SerializeObject(centre));
            RestResponse restResponse = restClient.Post(request);
            if (restResponse.Content.Equals("logged in"))
            {
                return Ok(restResponse.Content);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
