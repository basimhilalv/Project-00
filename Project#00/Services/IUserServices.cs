using Project_00.Models;

namespace Project_00.Services
{
    public interface IUserServices
    {
        public Task<User> RegisterUser(UserDto request);
        public Task<User> LoginUser(UserDto request);
    }
}
