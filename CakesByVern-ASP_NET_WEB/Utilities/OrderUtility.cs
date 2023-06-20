using CakesByVern_ASP_NET_WEB.Models;
using CakesByVern_Data.Database;
using CakesByVern_Data.Entity;
using CakesByVern_Data.Extensions;
using System.Text;

namespace CakesByVern_ASP_NET_WEB.Utilities
{
    public class OrderUtility
    {
        public static void PlaceOrder(IDataRepository _repository,PlaceOrderModel orderRequest, User? user, IConfiguration _configuration)
        {

            if (user == null)
            {
                user = new User(
                    _repository.GetAllUsers().Count() + 1,
                    orderRequest.FullName,
                    new DateOnly(),
                    orderRequest.Email,
                    "CLIENT",
                    new CakesByVern_Data.Security.Credential(
                        (orderRequest.Email + "User").MD5Hash(),
                        (orderRequest.Email + "Password").MD5Hash()
                        ));
                _repository.RegisterUser(user);
            }
            
            StringBuilder sb = new StringBuilder();
            sb.Append(orderRequest.ContactNumber + "~");
            sb.Append(orderRequest.DeliveryAddress + "~");
            sb.Append(orderRequest.AdditionalInformation + "~");
            sb.Append(orderRequest.ModeOfPayment + "~");
            sb.Append(orderRequest.TypeOfDelivery + "~");
            sb.Append(orderRequest.ProductPrice + "~");
            sb.Append(orderRequest.DeliveryDate);

            Order order = Order.Create(user, _repository.GetProduct(orderRequest.ProductId), DateTime.Now, sb.ToString());

            _repository.AddOrder(order);

            var mailData = new MailData
            {
                Subject = "Order Request",
                Body = OrderUtility.HTMLBodyParser(orderRequest),
                To = user.Email
            };
            var admins = _repository.GetAllUsers().Where(u => u.Role == "ADMIN");
            List<string> adminEmails = new List<string>();

            foreach (var admin in admins)
            {
                adminEmails.Add(admin.Email);
            }

            new EmailProviderAPI(_configuration).SendEmail(mailData, adminEmails);


        }

        public static string HTMLBodyParser(PlaceOrderModel order)
        {
            return $@"<a style=""font-weight: 999;font-size:18px;"">Buyer Information</a>
                        <a style=""font-weight: 999;"">Buyer: </a>
                        {order.FullName}
                        <a style=""font-weight: 999;"">Delivery Address: </a>
                        <a style=""color: orangered"">{order.DeliveryAddress}</a>
                        <a style=""font-weight: 999;"">Contact Number: </a>
                        {order.ContactNumber}

                        <a style=""font-weight: 999;font-size:18px;"">Order Details</a>
                        <a style=""font-weight: 999;"">Product:</a>
                        {order.ProductName}
                        <a style=""font-weight: 999;"">Quantity:</a>
                        1pc
                        <a style=""font-weight: 999;"">Price:</a>
                        ₱{order.ProductPrice}
                        <a style=""font-weight: 999;"">Message:</a>
                        <a style=""color: orangered"">{order.AdditionalInformation}</a>

                        <a style=""font-weight: 999;"">Mode of Payment:</a>
                        {order.ModeOfPayment}
                        <a style=""font-weight: 999;"">Delivery Date:</a>
                        {order.DeliveryDate.ToShortDateString()}
                        <a style=""font-weight: 999;"">Type of Delivery:</a>
                        {order.TypeOfDelivery}

                        <a style=""font-weight: 999;"">Total Amount:</a>
                        ₱{order.ProductPrice}";
        }
    }
}
