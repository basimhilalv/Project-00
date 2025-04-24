using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_00.Dtos;
using Project_00.Models;
using Project_00.Services;
using Project_00.Services.Interfaces;
using System.Security.Claims;

namespace Project_00.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressServices _addressServices;
        public AddressController(IAddressServices addressServices)
        {
            _addressServices = addressServices;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return Unauthorized();
            }
            Guid userIdguid = Guid.Parse(userId);
            var addresses = await _addressServices.GetAllAddress(userIdguid);
            if (addresses is null) return NotFound("There are no Address available");
            return Ok(addresses);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return Unauthorized();
            }
            Guid userIdguid = Guid.Parse(userId);
            var address = await _addressServices.GetAddress(id, userIdguid);
            if (address is null) return NotFound("Category Not Found");
            return Ok(address);
        }
        [Authorize]
        [HttpPost("Create")]
        public async Task<ActionResult<Address>> AddAddress(AddressDto request)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return Unauthorized();
            }
            Guid userIdguid = Guid.Parse(userId);
            var create = await _addressServices.AddAddress(request,userIdguid);
            if (create is null) return BadRequest();
            return Ok(create);
        }
        [Authorize]
        [HttpPut("Update")]
        public async Task<ActionResult<Address>> UpdateCategory(int id, AddressDto request)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return Unauthorized();
            }
            Guid userIdguid = Guid.Parse(userId);
            var update = await _addressServices.UpdateAddress(id, request, userIdguid);
            if (update is null) return BadRequest();
            return Ok(update);
        }
        [Authorize]
        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return Unauthorized();
            }
            Guid userIdguid = Guid.Parse(userId);
            var delete = await _addressServices.DeleteAddress(id,userIdguid);
            if (delete is null) return NotFound();
            return NoContent();
        }
    }
}
