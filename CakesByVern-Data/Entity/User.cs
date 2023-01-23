using CakesByVern_Data.Security;

namespace CakesByVern_Data.Entity
{
    public class User
    {
        public string Id { get; private set; }  
        public string FullName { get; private set; }
        public DateTime Birthdate { get; private set; }
        public string Email { get; private set; }
        public Credential Credential { get; private set; }

        public User(string id, string fullname, DateTime birthdate, string email, Credential credential)
        {
            Id = id;
            FullName = fullname;
            Birthdate = birthdate;
            Email = email;
            Credential = credential;
        }
        public static User Create(string id, string fullname, DateTime birthdate, string email, Credential credential) {
            return new User(id, fullname, birthdate, email, credential);
        }
    }
}
