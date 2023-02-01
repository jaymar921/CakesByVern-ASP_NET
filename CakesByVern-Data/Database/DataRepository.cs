using CakesByVern_Data.Entity;
using Microsoft.Extensions.Configuration;

namespace CakesByVern_Data.Database
{
    public class DataRepository : IDataRepository
    {
        private readonly SQLConnector _connector;

        public DataRepository(IConfiguration configuration)
        {
            _connector = new SQLConnector(configuration);
        }


        public void AddOrder(Order order)
        {
            string sqlFormattedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            string query = $"INSERT INTO orders (`user_id`,`product_id`,`orderdate`,`info`) values ({order.Buyer.Id}, {order.Product.Id}, '{sqlFormattedDate}', '{order.Information}')";

            try
            {
                _connector.ExecuteQuery(query);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Adding Order --> Err msg: {e.Message}");
            }
        }

        public int AddPost(Post post)
        {
            string sqlFormattedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            string query = $"INSERT INTO post (`title`,`description`,`author`,`created`,`updated`) values ('{post.Title}', '{post.Description}', '{post.Author}', '{sqlFormattedDate}', '{sqlFormattedDate}')";

            try
            {
                _connector.ExecuteQuery(query);
                using (var reader = _connector.ExecuteQueryReturn($"SELECT id FROM post WHERE created = '{sqlFormattedDate}'"))
                {
                    if (reader.Read())
                    {
                        int value = (int)reader[0];
                        reader.Close();
                        return value;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error Adding Post --> Err msg: {e.Message}");
            }
            return -1;
        }

        public int AddProduct(Product product)
        {
            string query = $"INSERT INTO `product` (`name`,`description`,`price`) values ('{product.Name}', '{product.Description}', {product.Price})";
            try
            {
                _connector.ExecuteQuery(query);
                using (var reader = _connector.ExecuteQueryReturn($"SELECT id FROM `product` WHERE name='{product.Name}'"))
                {
                    if (reader.Read())
                    {
                        int value = (int)reader[0];
                        reader.Close();
                        return value;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Adding Product --> Err msg: {e.Message} | {product.Name}");
            }
            return -1;
        }

        public bool DeleteOrder(int id)
        {
            string query = $"DELETE FROM orders WHERE id = {id}";

            try
            {
                _connector.ExecuteQuery(query);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeletePost(int id)
        {
            string query = $"DELETE FROM post WHERE id = {id}";

            try
            {
                _connector.ExecuteQuery(query);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteProduct(int id)
        {
            string query = $"DELETE FROM product WHERE id = {id}";

            try
            {
                _connector.ExecuteQuery(query);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteUser(int id)
        {
            string query = $"DELETE FROM user WHERE id = {id}";

            try
            {
                _connector.ExecuteQuery(query);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Order> GetAllOrders()
        {
            string query = $"SELECT * FROM orders";
            List<Order> orders = new List<Order>();

            using (var reader = _connector.ExecuteQueryReturn(query))
            {

                while (reader.Read())
                {
                    string? date = Convert.ToString(reader[3]);
                    var dt = DateTime.Parse(date != null ? date : "");
                    var order = new Order((int)reader[0], GetUserById((int)reader[1]), GetProduct((int)reader[2]), dt, (string)reader[4]);
                    orders.Add(order);
                }

            }
            return orders;
        }

        public IEnumerable<Post> GetAllPosts()
        {
            string query = $"SELECT * FROM post WHERE id >= (SELECT min(id) from post)-10 ORDER BY id DESC limit 10;";
            List<Post> posts = new List<Post>();

            using (var reader = _connector.ExecuteQueryReturn(query))
            {
                
                while (reader.Read())
                {
                    posts.Add(new Post
                    {
                        Id = (int)reader[0],
                        Title = (string)reader[1],
                        Description= (string)reader[2],
                        Author= (string)reader[3],
                        Created = DateTime.Parse(Convert.ToString(reader[4]) ?? DateTime.Now.ToString()),
                        Updated = DateTime.Parse(Convert.ToString(reader[5]) ?? DateTime.Now.ToString())
                    });
                }
                
            }
            return posts;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            string query = $"SELECT * FROM product";
            List<Product> posts = new List<Product>();

            using (var reader = _connector.ExecuteQueryReturn(query))
            {

                while (reader.Read())
                {
                    posts.Add(new Product
                    {
                        Id = (int)reader[0],
                        Name = (string)reader[1],
                        Description = (string)reader[2],
                        Price = Double.Parse(reader[3].ToString() ?? "0")
                    });
                }

            }
            return posts;
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Order GetOrder(int id)
        {
            string query = $"SELECT * FROM orders where id={id}";
            using (var reader = _connector.ExecuteQueryReturn(query))
            {
                if (reader.Read())
                {
                    string? date = Convert.ToString(reader[3]);
                    var dt = DateTime.Parse(date != null ? date : "");
                    var order = new Order((int)reader[0], GetUserById((int)reader[1]), GetProduct((int)reader[2]), dt, (string)reader[4]);
                    reader.Close(); return order;
                }
            }
            return Order.Empty();
        }

        public Post GetPost(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(int id)
        {
            string query = $"SELECT * FROM product where id={id}";
            using (var reader = _connector.ExecuteQueryReturn(query))
            {
                if (reader.Read())
                {
                    var product = new Product
                    {
                        Id = (int)reader[0],
                        Name = (string)reader[1],
                        Description = (string)reader[2],
                        Price = Double.Parse(reader[3].ToString() ?? "0")
                    };
                    reader.Close(); return product;
                }
            }
            return new Product { Id = -1 };
        }

        public User? GetUser(string username, string password)
        {

            string query = $"SELECT * FROM user where username='{username}' and password='{password}'";
            using(var reader = _connector.ExecuteQueryReturn(query))
            {
                if(reader.Read())
                {
                    string? date = Convert.ToString(reader[2]);
                    var dt = DateTime.Parse(date != null ? date : "");
                    User user = new User((int)reader[0], (string)reader[1], DateOnly.FromDateTime(dt), (string)reader[3], (string)reader[4], new Security.Credential((string)reader[5], (string)reader[6]));
                    reader.Close();
                    return user;
                }
            }
            return null;
        }

        public User? GetUser(string username)
        {

            string query = $"SELECT * FROM user where username='{username}'";
            using (var reader = _connector.ExecuteQueryReturn(query))
            {
                if (reader.Read())
                {
                    string? date = Convert.ToString(reader[2]);
                    var dt = DateTime.Parse(date != null ? date : "");
                    User user = new User((int)reader[0], (string)reader[1], DateOnly.FromDateTime(dt), (string)reader[3], (string)reader[4], new Security.Credential((string)reader[5], (string)reader[6]));
                    reader.Close();
                    return user;
                }
            }
            return null;
        }

        public User? GetUserByEmail(string email)
        {
            string query = $"SELECT * FROM user where email='{email}'";
            using (var reader = _connector.ExecuteQueryReturn(query))
            {
                if (reader.Read())
                {
                    string? date = Convert.ToString(reader[2]);
                    var dt = DateTime.Parse(date != null ? date : "");
                    User user = new User((int)reader[0], (string)reader[1], DateOnly.FromDateTime(dt), (string)reader[3], (string)reader[4], new Security.Credential((string)reader[5], (string)reader[6]));
                    reader.Close();
                    return user;
                }
            }
            return null;
        }

        public User GetUserById(int id)
        {
            string query = $"SELECT * FROM user where id='{id}'";
            using (var reader = _connector.ExecuteQueryReturn(query))
            {
                if (reader.Read())
                {
                    string? date = Convert.ToString(reader[2]);
                    var dt = DateTime.Parse(date != null ? date : "");
                    User user = new User((int)reader[0], (string)reader[1], DateOnly.FromDateTime(dt), (string)reader[3], (string)reader[4], new Security.Credential((string)reader[5], (string)reader[6]));
                    reader.Close();
                    return user;
                }
            }
            return User.Empty();
        }

        public bool RegisterUser(User user)
        {
            string sqlFormattedDate = DateTime.Parse(user.Birthdate.ToString()).ToString("yyyy-MM-dd");
            string query = $"INSERT INTO user (`fullname`,`birthdate`,`email`,`role`,`username`,`password`) values ('{user.FullName}', '{sqlFormattedDate}', '{user.Email}', '{user.Role}', '{user.Credential.UserName}', '{user.Credential.Password}')";

            User? existing = GetUserByEmail(user.Email);
            if (existing != null)
            {
                query = $"UPDATE user SET fullname='{user.FullName}', birthdate='{sqlFormattedDate}', role='{user.Role}', username='{user.Credential.UserName}', password='{user.Credential.Password}' WHERE email='{user.Email}'";
            }

            try
            {
                _connector.ExecuteQuery(query);
                return true;
            }catch(Exception ex) 
            {
                Console.WriteLine("Error Registering new User --> Err msg: "+ex.Message);
                return false;
            }
        }

        public bool UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePost(Post post)
        {
            throw new NotImplementedException();
        }

        public bool UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
