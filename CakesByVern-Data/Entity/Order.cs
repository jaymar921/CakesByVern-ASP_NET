using System.ComponentModel.DataAnnotations;

namespace CakesByVern_Data.Entity
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public User Buyer { get; set; }
        public Product Product { get; set; }
        public DateTime OrderDate { get; set; }
        public string Information { get; set; }
        public Order(User buyer, Product product, DateTime orderDate, string information)
        {
            Buyer = buyer;
            Product = product;
            OrderDate = orderDate;
            Information = information;
        }

        public Order()
        {
            Buyer = User.Empty();
            Product = new Product();
            OrderDate = DateTime.Now;
            Information = "";
        }
        public static Order Create(User buyer, Product product, DateTime orderDate, string Information)
        {
            return new Order(buyer, product, orderDate, Information);
        }

        public static Order Empty()
        {
            return new Order(new User(-1, "", new DateOnly(), "", "", new Security.Credential("", "")), new Product(), DateTime.Now, "");
        }
    }
}
