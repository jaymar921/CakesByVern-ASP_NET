using System.ComponentModel.DataAnnotations;

namespace CakesByVern_ASP_NET_WEB.Models
{
    public class PostModel
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;

        [DataType(DataType.Upload)]
        [Display(Name = "Upload image")]
        public IFormFile imageFile { get; set; }
    }
}
