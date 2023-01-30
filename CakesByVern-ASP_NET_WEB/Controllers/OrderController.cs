using CakesByVern_ASP_NET_WEB.Models;
using CakesByVern_ASP_NET_WEB.Models.Auth;
using CakesByVern_ASP_NET_WEB.Utilities;
using CakesByVern_Data.Database;
using CakesByVern_Data.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CakesByVern_ASP_NET_WEB.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataRepository _repository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public OrderController(IDataRepository repository, IWebHostEnvironment webHostEnvironment, ILogger<HomeController> logger)
        {
            _repository = repository;
            _hostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new OrderViewModel(_repository));
        }

        public async Task<IActionResult> AddProduct(ProductModel product)
        {
            if(product == null)
            {
                return View(new OrderViewModel(_repository));
            }
            _logger.LogInformation("Adding new product: "+product.Name + " | " + product.Description + " | " + product.Price +" | ");
            int id = _repository.AddProduct(new CakesByVern_Data.Entity.Product { Name = product.Name, Description = product.Description, Price = product.Price });

            try
            {
                // save the file

                // grab the root path, we have to save the files there
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string filename = (id + "_" + product.Name).MD5Hash() + ".png";

                string path = Path.Combine(wwwRootPath + "/SERVER_FILES/PRODUCTS/", filename);
                
                // save the file now in fileStream
                using(var fileStream = new FileStream(path, FileMode.Create))
                {
                    if(product.imageFile != null)
                        await product.imageFile.CopyToAsync(fileStream);
                }
            }catch(Exception ex)
            {
                _logger.LogError($"Error --> {ex.Message}");
            }
            return Redirect("/Order");
        }

        [Authorize]
        public IActionResult DeleteProduct(int id)
        {
            _repository.DeleteProduct(id);
            return Redirect("/Order");
        }

        
    }
}
