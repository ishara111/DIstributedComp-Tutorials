using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using Website.Models;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        private RestClient db;
        public IActionResult Index()
        {
            ViewBag.Title = "Status";

            db = new RestClient("https://localhost:44379/");

            RestRequest restRequest = new RestRequest("api/client", Method.Get);
            //restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
            RestResponse restResponse = db.Execute(restRequest);
            var clients = JsonConvert.DeserializeObject<List<ClientModel>>(restResponse.Content);

            RestRequest restRequest1 = new RestRequest("api/jobstate", Method.Get);
            //restRequest.AddJsonBody(JsonConvert.SerializeObject(data));
            RestResponse restResponse1 = db.Execute(restRequest1);
            var jobs = JsonConvert.DeserializeObject<List<JobStateModel>>(restResponse1.Content);

            var list = new List<ShowModel>();
            string comp = "";
            int jobid = 0;

            foreach (var c in clients)
            {
                ShowModel show = new ShowModel();
                show.ip = c.ip;
                show.Port = (int)c.port;
                show.count = (int)c.count;
                show.Complete = "No Job";
                foreach (var j in jobs)
                {
                    if (c.Id.Equals(j.clientId))
                    {
                        if (j.state == true)
                        {
                            show.Complete = "Complete";
                        }
                        else if (j.state == false)
                        {
                            show.Complete = "Not Complete";
                        }
                    }
                    else
                    {
                        show.Complete = "No Job";
                    }
                }


                //foreach (var j in jobs)
                //{
                //    if (j.Id.Equals(jobid))
                //    {
                //        if (j.state==true)
                //        {
                //            show.Complete = "Complete";
                //        }
                //        else if (j.state==false)
                //        {
                //            show.Complete = "Not Complete";
                //        }
                //    }
                //    else
                //    {
                //        show.Complete = "No Job";
                //    }
                //}

                list.Add(show);
            }



            return View(list);
        }

    }
}
