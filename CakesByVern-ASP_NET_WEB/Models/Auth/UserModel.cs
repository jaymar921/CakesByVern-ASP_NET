using System.ComponentModel.DataAnnotations;

namespace CakesByVern_ASP_NET_WEB.Models.Auth
{
    public class UserModel
    {
        [RegularExpression("[a-zA-Z\\s]+")]
        public string Fullname { get; set; } = string.Empty;
        public DateTime Birthdate { get; set; }
        [RegularExpression("[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2, 4}$")]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
