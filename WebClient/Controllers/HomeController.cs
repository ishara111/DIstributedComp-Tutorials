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
            return View(ShowAllCentres());
        }

        [HttpPost]
        public IActionResult Login(string username,string password)
        {
            RestClient restClient = new RestClient("https://localhost:44363/");
            RestRequest request = new RestRequest("api/login?username="+username+"&password="+password);
            //restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
            RestResponse restResponse = restClient.Post(request);
            string res = JsonConvert.DeserializeObject<string>(restResponse.Content);
            if (res.Equals("logged in"))
            {
                return Ok(restResponse.Content);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public List<Centre> ShowAllCentres()
        {
            RestClient restClient = new RestClient("https://localhost:44363/");
            RestRequest request = new RestRequest("api/centres");
            RestResponse resp = restClient.Get(request);
            RestResponse restResponse = restClient.Execute(request);
            List<Centre> centreList = JsonConvert.DeserializeObject<List<Centre>>(restResponse.Content);
            return centreList;
        }

        [HttpGet]
        public string GetCentreBookings(int centreId)
        {
            RestClient restClient = new RestClient("https://localhost:44363/");
            RestRequest request = new RestRequest("api/CentreBookings/"+centreId);
            RestResponse resp = restClient.Get(request);
            RestResponse restResponse = restClient.Execute(request);
            if (restResponse.Content!=null || restResponse.Content!="")
            {
                List<Booking> bookingList = JsonConvert.DeserializeObject<List<Booking>>(restResponse.Content);
                if (bookingList!=null)
                {
                    string s = "Name | Start Date | End Date \n";
                    foreach (var b in bookingList)
                    {
                        DateTime sd = (DateTime)b.startDate;
                        DateTime ed = (DateTime)b.endDate;
                        s = s + ("" + b.name + " | " + sd.Date.ToString("dd/MM/yyyy") + " | " + ed.Date.ToString("dd/MM/yyyy") + "\n");
                    }
                    return s;
                }
                return "No bookings for centre";

            }
            return "No bookings for centre";

        }

        [HttpPost]
        public IActionResult AddCentre([FromBody] Centre centre)
        {
            RestClient restClient = new RestClient("https://localhost:44363/");
            RestRequest request = new RestRequest("api/centres");
            request.AddJsonBody(JsonConvert.SerializeObject(centre));
            RestResponse restResponse = restClient.Post(request);
            return Ok();
        }

        [HttpPost]
        public IActionResult Book([FromBody] Booking booking)
        {
            DateTime now = DateTime.Now;
            if (booking.startDate>now)
            {
                RestClient restClient = new RestClient("https://localhost:44363/");
                RestRequest request = new RestRequest("api/bookings");
                request.AddJsonBody(JsonConvert.SerializeObject(booking));
                try
                {
                    RestResponse restResponse = restClient.Post(request);
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }



        }

        [HttpGet]
        public string GetNextDate(int centreId)
        {
            RestClient restClient = new RestClient("https://localhost:44363/");
            RestRequest request = new RestRequest("api/nextdate/" + centreId);
            RestResponse resp = restClient.Get(request);
            RestResponse restResponse = restClient.Execute(request);
            if(restResponse.Content.ToString()!="")
            {
                DateTime date = JsonConvert.DeserializeObject<DateTime>(restResponse.Content);
                return date.Date.ToString("dd/MM/yyyy");
            }
            return "Not found";
        }

        [HttpGet]
        public string SearchCentre(string search)
        {
            RestClient restClient = new RestClient("https://localhost:44363/");
            RestRequest request = new RestRequest("api/centres");
            RestResponse resp = restClient.Get(request);
            RestResponse restResponse = restClient.Execute(request);
            List<Centre> centreList = JsonConvert.DeserializeObject<List<Centre>>(restResponse.Content);
            
            foreach (var c in centreList)
            {
                if(c.centreName.Contains(search))
                {
                    return c.centreName;
                }
            }
            return "Not Found";
        }
    }
}
