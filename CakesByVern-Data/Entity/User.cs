using CakesByVern_Data.Security;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net;

namespace CakesByVern_Data.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }  
        public string FullName { get; set; } = String.Empty;
        public DateOnly Birthdate { get; private set; }
        public string Email { get; set; } = String.Empty;
        public string Role { get; set; }
        public Credential Credential { get; private set; }

        public User(int id, string fullname, DateOnly birthdate, string email, string role, Credential credential)
        {
            Id = id;
            FullName = fullname;
            Birthdate = birthdate;
            Email = email;
            Credential = credential;
            Role = role;
        }
        public static User Create(int id, string fullname, DateOnly birthdate, string email, string role, Credential credential) {
            return new User(id, fullname, birthdate, email, role, credential);
        }

        public User()
        {
            Id = -1;
            FullName = "";
            Birthdate = new DateOnly();
            Email = "";
            Credential = new Credential("", "");
            Role = "";
        }

        public static User Empty()
        {
            return new User(-1, "", new DateOnly(), "", "", new Credential("", ""));
        }
    }
}
