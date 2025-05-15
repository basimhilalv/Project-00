using Project_00.Dtos;
using Project_00.Models;

namespace Project_00.Services.UserService
{
    public interface IUserServices
    {
        public Task<string> RegisterUser(UserDto request);
        public Task<string> LoginUser(UserDto request);
        public Task<UserResponseDto> GetUsers();
        public Task<UserResponseDto> GetUserById(Guid id);
        public Task<string> BlockUser(Guid id);
        public Task<string> UnblockUser(Guid id);
    }
}
