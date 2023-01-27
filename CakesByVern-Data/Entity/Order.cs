namespace CakesByVern_Data.Entity
{
    public class Order
    {
        public int Id { get; private set; }
        public User Buyer { get; private set; }
        public Product Product { get; private set; }
        public DateTime OrderDate { get; private set; }
        public Order(int id, User buyer, Product product, DateTime orderDate)
        {
            Id = id;
            Buyer = buyer;
            Product = product;
            OrderDate = orderDate;
        }
        public static Order Create(int id, User buyer, Product product, DateTime orderDate)
        {
            return new Order(id, buyer, product, orderDate);
        }
    }
}
