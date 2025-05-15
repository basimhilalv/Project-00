using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project_00.Data;
using Project_00.Dtos;
using Project_00.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project_00.Services.UserService
{
    public class UserServices : IUserServices
    {
        private readonly Context _context;

        public UserServices(Context context)
        {
            _context = context;
        }

        public async Task<string> BlockUser(Guid id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Role == "User" && u.Id == id);
                if (user is null) return null;
                if (!user.IsActive) return "User is already blocked";
                user.IsActive = false;
                await _context.SaveChangesAsync();
                return "User is blocked";
            }catch(Exception ex)
            {
                throw new Exception("Error occured while blocking user");
            }
        }

        public async Task<UserResponseDto> GetUserById(Guid id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user is null)
                {
                    var usererror = new UserResponseDto()
                    {
                        Message = "There is no User available",
                        Data = null,
                        Error = true
                    };
                    return usererror;
                }
                var userresponse = new UserResponseDto()
                {
                    Message = "User is available here",
                    Data = (IEnumerable<User>)user,
                    Error = false
                };
                return userresponse;

            }
            catch(Exception ex)
            {
                throw new Exception("Error occured while fetching data", ex);
            }
        }

        public async Task<UserResponseDto> GetUsers()
        {
            try
            {
                var users = await _context.Users.Where(u=> u.Role == "User").ToListAsync();
                if (users is null || !users.Any())
                {
                    var usererror = new UserResponseDto()
                    {
                        Message = "There are no Users available",
                        Data = null,
                        Error = true
                    };
                    return usererror;
                }
                var user = new UserResponseDto()
                {
                    Message = "List of available users",
                    Data = users,
                    Error = false
                };
                return user;
            }
            catch(Exception ex)
            {
                throw new Exception("Error occured while fetching users", ex);
            }
        }

        public async Task<string> LoginUser(UserDto request)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username && u.IsActive);
                if (user is null) return null;
                if (!BCrypt.Net.BCrypt.Verify(request.Password,user.Password)) return null;
                var token = CreateToken(user);
                return token;
            }
            catch(Exception ex)
            {
                throw new Exception("Error occured while fetching data", ex);
            }
        }

        public async Task<string> RegisterUser(UserDto request)
        {
            try
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

            }catch(Exception ex)
            {
                throw new Exception("Error occured while fetching data", ex);
            }
        }

        public async Task<string> UnblockUser(Guid id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Role == "User" && u.Id == id);
                if (user is null) return null;
                if (user.IsActive) return "User is already unblocked";
                user.IsActive = true;
                await _context.SaveChangesAsync();
                return "User is unblocked";
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while unblocking user");
            }
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
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
