using authenticationJWT.Models;
using AuthenticationJWT.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace authenticationJWT.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context) => _context = context;

        public User Create(User user)
        {
            _context.Users.Add(user);
            user.Id = _context.SaveChanges();

            return user;
        }

        public User GetByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email.Equals(email));

            return user;
        }

        public User GetById(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id.Equals(id));

            return user;
        }

        public List<User> GetAll()
        {
            var users = _context.Users.AsNoTracking().ToList();

            return users;
        }
    }
}