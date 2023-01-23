namespace CakesByVern_Data.Security
{
    public class Credential
    {
        public string UserName { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public Guid CredentialId { get; private set; } = Guid.NewGuid();

        public Credential(Guid uid, string username, string password) 
        { 
            CredentialId = uid;
            UserName = username;
            Password = password;
        }

    }
}
