// Models-UserService.cs
using ShopAppProject.Models;

namespace ShopAppProject.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

       public User GetUserById(string userId)
        {
            return _context.Users.FirstOrDefault(u => u.Id == userId);
        }
        public void UpdateUser(User updatedUser)
        {
            _context.Users.Update(updatedUser);
            _context.SaveChanges();
        }
    }
}
