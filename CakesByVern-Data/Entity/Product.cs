using System.ComponentModel.DataAnnotations;

namespace CakesByVern_Data.Entity
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public double Price { get; set; }

    }
}
