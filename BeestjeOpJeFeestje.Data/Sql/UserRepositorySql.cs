using BeestjeOpJeFeestje.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BeestjeOpJeFeestje.Domain.Sql
{
    public class UserRepositorySql : IUserRepository
    {
        readonly BeestjeOpJeFeestjeContext _context;

        public UserRepositorySql(BeestjeOpJeFeestjeContext context)
        {
            _context = context;
        }

        public User CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public List<IdentityRole> GetAlleRoles() => _context.Roles.ToList();

        public User GetUserByMail(string email) => _context.Users.FirstOrDefault(u => u.Email == email);
    }
}
