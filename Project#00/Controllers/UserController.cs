using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_00.Models;
using Project_00.Services;

namespace Project_00.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<string>> loginUser(UserDto request)
        {
            var result = await _userServices.LoginUser(request);
            if (result == null) return BadRequest();
            return Ok(result);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<string>> registerUser(UserDto request)
        {
            var result = await _userServices.RegisterUser(request);
            if (result == null) return BadRequest();
            return Ok(result);
        }
        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<User>>> getusers()
        {
            var result = await _userServices.GetUsers();
            if (result is null) return NotFound();
            return Ok(result);
        }
        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult<User>> getuserbyId(Guid id)
        {
            var result = await _userServices.GetUserById(id);
            if (result is null) return NotFound();
            return Ok(result);
        }
    }
}
