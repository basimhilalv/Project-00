using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project_00.Data;
using Project_00.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project_00.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserDbContext _context;

        public UserServices(UserDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserById(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null) return null;
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            if (users is null) return null;
            return users;
        }

        public async Task<string> LoginUser(UserDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user is null) return null;
            if (!BCrypt.Net.BCrypt.Verify(request.Password,user.Password)) return null;
            var token = CreateToken(user);
            return token;
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

        private string CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("supersecretkeyofbasimhilalisfamousforeverythingthattoughtinthiseraoftechnologyandresearchokthenbye");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
