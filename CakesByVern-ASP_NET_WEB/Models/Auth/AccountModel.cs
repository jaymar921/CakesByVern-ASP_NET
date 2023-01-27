namespace CakesByVern_ASP_NET_WEB.Models.Auth
{
    public class AccountModel
    {
        public Guid uid { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string ReturnUrl { get; set; } = "/";
    }
}
