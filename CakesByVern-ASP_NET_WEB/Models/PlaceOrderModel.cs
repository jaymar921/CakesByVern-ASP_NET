using CakesByVern_ASP_NET_WEB.Models.Auth;
using System.ComponentModel.DataAnnotations;

namespace CakesByVern_ASP_NET_WEB.Models
{
    public class PlaceOrderModel
    {
        public int ProductId { get; set; } = 0;
        public string FullName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        [RegularExpression("^((09)|(\\+639))[0-9]{9}$")]
        public string ContactNumber { get; set; } = string.Empty;
        [RegularExpression("^[a-zA-Z0-9._]+@[a-zA-Z]+\\.[a-zA-Z]+$")]
        public string Email { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;
        public string AdditionalInformation { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Select the mode of payment")]
        public string ModeOfPayment { get; set; } = string.Empty;
        [Required(ErrorMessage = "Select the type of delivery")]
        public string TypeOfDelivery { get; set;} = string.Empty;
        public double ProductPrice { get; set; }
        public DateTime DeliveryDate { get; set; }



    }
}
