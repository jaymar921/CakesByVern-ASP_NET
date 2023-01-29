using CakesByVern_ASP_NET_WEB.Utilities;
using CakesByVern_Data.Database;

namespace CakesByVern_ASP_NET_WEB.Models
{
    public class OrderViewModel
    {
        private readonly IDataRepository _repository;

        public OrderViewModel(IDataRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ProductModel> Products() => _repository.GetAllProductModel();
    }
}
