﻿using Project_00.Dtos;
using Project_00.Models;

namespace Project_00.Services.Interfaces
{
    public interface IUserServices
    {
        public Task<string> RegisterUser(UserDto request);
        public Task<string> LoginUser(UserDto request);
        public Task<UserResponseDto> GetUsers();
        public Task<UserResponseDto> GetUserById(Guid id);
    }
}
