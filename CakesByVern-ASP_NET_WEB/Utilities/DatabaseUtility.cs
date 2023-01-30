using CakesByVern_ASP_NET_WEB.Models;
using CakesByVern_Data.Database;
using CakesByVern_Data.Extensions;

namespace CakesByVern_ASP_NET_WEB.Utilities
{
    public static class DatabaseUtility
    {

        public static IEnumerable<PostModel> GetAllPostModel(this IDataRepository dataRepository)
        {
            var posts = dataRepository.GetAllPosts();
            List<PostModel> postsList = new List<PostModel>();
            foreach (var post in posts)
            {
                postsList.Add(new PostModel
                {
                    Id= post.Id,
                    Author = post.Author,
                    Title= post.Title,
                    Description = post.Description,
                    Created = post.Created,
                    Updated = post.Updated,
                    imageFileSrc = "/SERVER_FILES/POSTS/" + (post.Id + "_" + post.Title).MD5Hash() +".png"
                });
            }
            return postsList;
        }

        public static IEnumerable<ProductModel> GetAllProductModel(this IDataRepository dataRepository)
        {
            var products = dataRepository.GetAllProducts();
            List<ProductModel> productsList = new List<ProductModel>();
            
            foreach (var product in products)
            {
                productsList.Add(new ProductModel
                {
                    Id = product.Id,
                    Name= product.Name,
                    Description = product.Description,
                    Price= product.Price,
                    imageFileSrc = "/SERVER_FILES/PRODUCTS/" + (product.Id + "_" + product.Name).MD5Hash() + ".png"
                });
            }
            return productsList;
        }

        public static int GetProductIdHashed(this IDataRepository dataRepository, string hashedId)
        {
            int id = 0;
            while(true)
            {
                if (id == 999999)
                    return -1;
                if (hashedId.Equals(dataRepository.GetProduct(id++).Id.ToString().MD5Hash()))
                    return id-1;
            }
        }
    }
}
