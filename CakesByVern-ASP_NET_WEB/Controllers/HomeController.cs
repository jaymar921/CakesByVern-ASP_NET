using CakesByVern_ASP_NET_WEB.Models;
using CakesByVern_ASP_NET_WEB.Utilities;
using CakesByVern_Data.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace CakesByVern_ASP_NET_WEB.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDataRepository _dataRepository;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IDataRepository dataRepository)
        {
            _logger = logger;
            _hostEnvironment = webHostEnvironment;
            _dataRepository = dataRepository;
        }

        public IActionResult Index()
        {
            HomePageModel model = new HomePageModel(_dataRepository);
            return View(model);
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

            // save the post into the database
            int id = _dataRepository.AddPost(new CakesByVern_Data.Entity.Post { Title = postModel.Title, Description = postModel.Description, Author = postModel.Author });

            try
            {
                // save the file
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = id + "_"+ postModel.Title;
                
                string path = Path.Combine(wwwRootPath+"/SERVER_FILES/POSTS/", fileName + ".png");
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    if(postModel.imageFile != null)
                        await postModel.imageFile.CopyToAsync(fileStream);
                }
            }catch(Exception ex) 
            {
                _logger.LogError($"Error --> {ex.Message}");
            }
            return Redirect("/");
        }

        [Authorize]
        [Route("/DeletePost/{id}")]
        public IActionResult DeletePost(int id)
        {
            _dataRepository.DeletePost(id);
            return Redirect("/");
        }
    }
}