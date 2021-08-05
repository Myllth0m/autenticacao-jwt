using authenticationJWT.Models;
using System.Collections.Generic;

namespace AuthenticationJWT.Data
{
    public interface IUserRepository
    {
        User Create(User user);

        User GetByEmail(string email);

        User GetById(int id);

        List<User> GetAll();
    }
}