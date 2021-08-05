using System.Text.Json.Serialization;

namespace authenticationJWT.Models
{
    public class User
    {
        public User(
            string name,
            string email,
            string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
    }
}