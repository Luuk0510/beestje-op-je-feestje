using Microsoft.AspNetCore.Identity;
using System.Dynamic;

namespace BeestjeOpJeFeestje.Domain.Interfaces
{
    public interface IUserRepository
    {
        public User CreateUser(User user);
        public List<IdentityRole> GetAlleRoles();

        public User GetUserByMail(string email);
    }
}
