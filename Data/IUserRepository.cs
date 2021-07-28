using authenticationJWT.Models;

namespace AuthenticationJWT.Data
{
    public interface IUserRepository
    {
        User Create(User user);

        User GetByEmail(string email);
    }
}