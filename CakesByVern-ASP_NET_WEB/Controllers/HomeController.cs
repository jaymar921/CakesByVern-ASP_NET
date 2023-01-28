using CakesByVern_ASP_NET_WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace CakesByVern_ASP_NET_WEB.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _hostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadPost(PostModel postModel)
        {
            if (postModel == null)
            {
                _logger.LogInformation("Nothing to save");
                TempData["Message"] = "There is no content to post";
                return View();
            }

            try
            {
                // save the file
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = postModel.Title;
                string extension = Path.GetExtension(postModel.imageFile.FileName);
                
                string path = Path.Combine(wwwRootPath+"/SERVER_FILES/POSTS/", fileName+extension);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await postModel.imageFile.CopyToAsync(fileStream);
                }
            }catch(Exception ex) 
            {
                _logger.LogError($"Error --> {ex.Message}");
            }
            return Redirect("/");
        }
    }
}