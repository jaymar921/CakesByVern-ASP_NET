namespace CakesByVern_ASP_NET_WEB.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public IFormFile? imageFile { get; set; } = null;
        public string imageFileSrc { get; set; } = string.Empty;
    }
}
