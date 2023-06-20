using CakesByVern_Data.Database;
using CakesByVern_Data.Entity;

namespace CakesByVern_ASP_NET_WEB.Data
{
    public class SqLiteDataRepository : IDataRepository
    {

        private readonly DatabaseContext _context;

        public SqLiteDataRepository(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;

            IConfigurationSection conf = configuration.GetSection("AdminEmails");
            conf.AsEnumerable().ToList().ForEach(x => { 
                var user = _context.Users.FirstOrDefault(u => u.Email == x.Value);

                if (user != null)
                {
                    if(user.Role != "ADMIN")
                    {
                        user.Role = "ADMIN";
                        _context.Users.Update(user);
                        _context.SaveChanges();
                    }
                }
            });
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public int AddPost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();


            return _context.Posts.FirstOrDefault(p => p.Author == post.Author && p.Title == post.Title && p.Created == post.Created)?.Id ?? -1;
        }

        public int AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product.Id;
        }

        public bool DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
                return false;
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return true;
        }

        public bool DeletePost(int id)
        {
            var post = _context.Posts.Find(id);
            if (post == null)
                return false;
            _context.Posts.Remove(post);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return false;
            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders;
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return _context.Posts;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users;
        }

        public Order GetOrder(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.Id == id) ?? Order.Empty();
        }

        public Post GetPost(int id)
        {
            return _context.Posts.Find(id) ?? new Post();
        }

        public Product GetProduct(int id)
        {
            return _context.Products.Find(id) ?? new Product();
        }

        public User? GetUser(string username, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Credential.UserName == username && u.Credential.Password == password);
        }

        public User? GetUser(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Credential.UserName == username);
        }

        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public bool RegisterUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
            return true;
        }

        public bool UpdatePost(Post post)
        {
            _context.Posts.Update(post);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                return false;
            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }

        public User GetUser(int id)
        {
            return _context.Users.Find(id) ?? User.Empty();
        }



    }
}
