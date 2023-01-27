namespace CakesByVern_ASP_NET_WEB.Models.Auth
{
    public class UserModel
    {
        public string Fullname { get; set; } = string.Empty;
        public DateTime Birthdate { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
