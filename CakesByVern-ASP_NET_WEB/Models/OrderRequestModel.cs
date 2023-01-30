using CakesByVern_ASP_NET_WEB.Models.Auth;
using System.ComponentModel.DataAnnotations;

namespace CakesByVern_ASP_NET_WEB.Models
{
    public class OrderRequestModel
    {
        public ProductModel Product { get; set; }
        public UserModel User { get; set; }
        [RegularExpression("^((09)|(\\+639))[0-9]{9}$")]
        public string ContactNumber { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;
        public string AdditionalInformation { get; set; } = string.Empty;

        public OrderRequestModel(ProductModel productModel, UserModel user) 
        {
            Product= productModel;
            User= user;
        }
    }
}
