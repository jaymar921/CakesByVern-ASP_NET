namespace CakesByVern_Data.Entity
{
    public class Order
    {
        public int Id { get; private set; }
        public User Buyer { get; private set; }
        public Product Product { get; private set; }
        public DateTime OrderDate { get; private set; }
        public string Information { get; private set; }
        public Order(int id, User buyer, Product product, DateTime orderDate, string information)
        {
            Id = id;
            Buyer = buyer;
            Product = product;
            OrderDate = orderDate;
            Information = information;
        }
        public static Order Create(int id, User buyer, Product product, DateTime orderDate, string Information)
        {
            return new Order(id, buyer, product, orderDate, Information);
        }

        public static Order Empty()
        {
            return new Order(-1, new User(-1, "", new DateOnly(), "", "", new Security.Credential("", "")), new Product(), DateTime.Now, "");
        }
    }
}
