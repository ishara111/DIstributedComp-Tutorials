using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Status";



            return View();
        }

    }
}
