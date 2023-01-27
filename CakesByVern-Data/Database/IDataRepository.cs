using CakesByVern_Data.Entity;

namespace CakesByVern_Data.Database
{
    public interface IDataRepository
    {

        // User associate
        bool RegisterUser(User user);
        User? GetUser(string username, string password);
        User? GetUser(string username);
        IEnumerable<User> GetAllUsers();
        bool UpdateUser(User user);
        bool DeleteUser(int id);

        // Post associate
        void AddPost(Post post);
        bool UpdatePost(Post post);
        bool DeletePost(int id);
        IEnumerable<Post> GetAllPosts();
        Post GetPost(int id);

        // Product associate
        void AddProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(int id);
        IEnumerable<Product> GetAllProducts();
        Product GetProduct(int id);

        // Order Associate
        void AddOrder(Order order);
        bool UpdateOrder(Order order);
        bool DeleteOrder(int id);
        IEnumerable<Order> GetAllOrders();
        Order GetOrder(int id);

    }
}
