using Microsoft.EntityFrameworkCore;
using Project_00.Data;
using Project_00.Models;

namespace Project_00.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserDbContext _context;

        public UserServices(UserDbContext context)
        {
            _context = context;
        }
        public async Task<string> LoginUser(UserDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user is null) return null;
            if (!BCrypt.Net.BCrypt.Verify(request.Password,user.Password)) return null;
            return "Login Successfull";
        }

        public async Task<string> RegisterUser(UserDto request)
        {
            if(await _context.Users.AnyAsync(u=> u.Username == request.Username))
            {
                return null;
            }

            var user = new User();
            user.Username = request.Username;
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return "User Registered";
        }
    }
}
