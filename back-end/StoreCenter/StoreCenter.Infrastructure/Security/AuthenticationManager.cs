using Microsoft.EntityFrameworkCore;
using StoreCenter.Infrastructure.Data;
using StoreCenter.Infrastructure.Interfaces;

namespace StoreCenter.Infrastructure.Security
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly ApplicationDbContext _context;

        public AuthenticationManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SigninUserAsync(string userName, string password)
        {
            try
            {
                //var users = _context.Users.ToList();
                // Get user by username
                var user = _context.Users.FirstOrDefault(u => u.UserName == userName);

                if (user == null)
                    return false;

                // Assuming passwords are hashed, you'd compare hashed passwords here
                return user.PasswordHash == password;
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                return false;
            }
        }
    }
}
