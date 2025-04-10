﻿using Project_00.Models;

namespace Project_00.Services
{
    public interface IUserServices
    {
        public Task<string> RegisterUser(UserDto request);
        public Task<string> LoginUser(UserDto request);
    }
}
