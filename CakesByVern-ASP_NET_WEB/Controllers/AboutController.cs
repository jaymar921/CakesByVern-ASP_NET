using Microsoft.AspNetCore.Mvc;

namespace CakesByVern_ASP_NET_WEB.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
