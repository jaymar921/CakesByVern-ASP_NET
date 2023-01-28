﻿using CakesByVern_Data.Entity;
using System;
using System.Globalization;

namespace CakesByVern_Data.Database
{
    public class DataRepository : IDataRepository
    {
        private readonly SQLConnector _connector;

        public DataRepository(SQLConnector connector)
        {
            _connector = connector;
        }


        public void AddOrder(Order order)
        {
            throw new NotImplementedException();
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
                        return (int)reader[0];
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error Adding Post --> Err msg: {e.Message}");
            }
            return -1;
        }

        public void AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public bool DeleteOrder(int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public bool DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAllPosts()
        {
            string query = $"SELECT * FROM post";
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
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Order GetOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Post GetPost(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(int id)
        {
            throw new NotImplementedException();
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
                    return new User((int)reader[0], (string)reader[1], DateOnly.FromDateTime(dt), (string)reader[3], (string)reader[4], new Security.Credential((string)reader[5], (string)reader[6]));
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
                    return new User((int)reader[0], (string)reader[1], DateOnly.FromDateTime(dt), (string)reader[3], (string)reader[4], new Security.Credential((string)reader[5], (string)reader[6]));
                }
            }
            return null;
        }

        public bool RegisterUser(User user)
        {
            string sqlFormattedDate = DateTime.Parse(user.Birthdate.ToString()).ToString("yyyy-MM-dd");
            string query = $"INSERT INTO user (`fullname`,`birthdate`,`email`,`role`,`username`,`password`) values ('{user.FullName}', '{sqlFormattedDate}', '{user.Email}', '{user.Role}', '{user.Credential.UserName}', '{user.Credential.Password}')";

            try
            {
                _connector.ExecuteQuery(query);
                return true;
            }catch
            {
                Console.WriteLine("Error Registering new User --> Err msg: ");
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
