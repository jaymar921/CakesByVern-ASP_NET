using CakesByVern_ASP_NET_WEB.Models.Auth;
using CakesByVern_ASP_NET_WEB.Models;
using CakesByVern_ASP_NET_WEB.Utilities;
using CakesByVern_Data.Database;
using CakesByVern_Data.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.Extensions.Logging;
using CakesByVern_Data.Entity;

namespace CakesByVern_ASP_NET_WEB.Controllers
{
    public class OrderRequestController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataRepository _repository;

        public OrderRequestController(IDataRepository repository, ILogger<HomeController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IActionResult Index(string id, string user)
        {
            if (string.IsNullOrEmpty(id))
                return Redirect("/Order");
            int orderId = _repository.GetProductIdHashed(id);
            var product = _repository.GetProduct(orderId);
            ProductModel productModel = new ProductModel
            {
                Id = orderId,
                Price = product.Price,
                Description = product.Description,
                Name = product.Name,
                imageFileSrc = "/SERVER_FILES/PRODUCTS/" + (orderId + "_" + product.Name).MD5Hash() + ".png"
            };
            var userObj = _repository.GetUser(user);
            UserModel userModel = new UserModel();
            if (userObj != null)
            {
                userModel.Email = userObj.Email;
                userModel.Fullname = userObj.FullName;
            }
            return View(new OrderRequestModel(productModel, userModel));
        }

        [HttpPost]
        public IActionResult OrderPlaced(PlaceOrderModel placeOrder)
        {
            if (placeOrder == null)
                return Redirect("/Order");

            var product = _repository.GetProduct(placeOrder.ProductId);
            placeOrder.ProductName = product.Name;
            placeOrder.ProductPrice = product.Price;
            return View(placeOrder);
        }

        [HttpPost]
        public IActionResult SaveOrderRequest(PlaceOrderModel orderRequest)
        {
            if (orderRequest == null)
                return Redirect("/Order");

            User? user = _repository.GetUserByEmail(orderRequest.Email);

            if(user == null)
            {
                _repository.RegisterUser(new User(
                    -1,
                    orderRequest.FullName,
                    new DateOnly(),
                    orderRequest.Email,
                    "CLIENT",
                    new CakesByVern_Data.Security.Credential(
                        (orderRequest.Email + "User").MD5Hash(),
                        (orderRequest.Email + "Password").MD5Hash()
                        )));
                user = _repository.GetUser((orderRequest.Email + "User").MD5Hash());
            }
            if(user != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(orderRequest.ContactNumber +"~");
                sb.Append(orderRequest.DeliveryAddress + "~");
                sb.Append(orderRequest.AdditionalInformation + "~");
                sb.Append(orderRequest.ModeOfPayment + "~");
                sb.Append(orderRequest.TypeOfDelivery + "~");
                sb.Append(orderRequest.ProductPrice + "~");
                sb.Append(orderRequest.DeliveryDate);
                Order order = Order.Create(-1, user, _repository.GetProduct(orderRequest.ProductId), DateTime.Now, sb.ToString());

                _repository.AddOrder(order);
            }
            
            
            return Redirect("/");
        }
    }
}
