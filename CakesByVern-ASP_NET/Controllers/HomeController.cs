using Microsoft.AspNetCore.Mvc;

namespace CakesByVern_ASP_NET.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
