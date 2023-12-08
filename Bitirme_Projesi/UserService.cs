// UserService.cs
using ShopCoreProject.Models;

namespace ShopCoreProject.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public User GetUserById(int userId)
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
