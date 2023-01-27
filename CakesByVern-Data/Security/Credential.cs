namespace CakesByVern_Data.Security
{
    public class Credential
    {
        public string UserName { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;

        public Credential(string username, string password) 
        { 
            UserName = username;
            Password = password;
        }

    }
}
