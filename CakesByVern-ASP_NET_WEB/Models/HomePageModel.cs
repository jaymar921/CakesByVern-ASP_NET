using CakesByVern_ASP_NET_WEB.Utilities;
using CakesByVern_Data.Database;

namespace CakesByVern_ASP_NET_WEB.Models
{
    public class HomePageModel
    {
        private readonly IDataRepository repository;
        

        public HomePageModel(IDataRepository dataRepository)
        {
            repository= dataRepository;
            Post = new PostModel();
        }

        public PostModel Post { get; set; }

        public IEnumerable<PostModel> Posts()
        {
            return repository.GetAllPostModel();
        }
    }
}
