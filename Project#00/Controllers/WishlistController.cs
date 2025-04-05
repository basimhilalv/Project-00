using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_00.Models;
using Project_00.Services;
using System.Security.Claims;

namespace Project_00.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistServices _wishlistServices;
        public WishlistController(IWishlistServices wishlistServices)
        {
            _wishlistServices = wishlistServices;
        }

        [Authorize]
        [HttpGet("GetWishlist")]
        public async Task<ActionResult<IEnumerable<Wishlist>>> GetWishlist()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return Unauthorized();
            }
            Guid userIdguid = Guid.Parse(userId);
            var items = await _wishlistServices.GetWishlist(userIdguid);
            if (items is null) return NotFound();
            return Ok(items);
        }
        [Authorize]
        [HttpPost("AddToWishlist")]
        public async Task<ActionResult<string>> Addtowishlist(WishlistDto request)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return Unauthorized();
            }
            Guid userIdguid = Guid.Parse(userId);
            var status = await _wishlistServices.AddToWishlist(request, userIdguid);
            if (status is null) return BadRequest();
            return Ok(status);
        }
        [Authorize]
        [HttpDelete("RemoveWishlist")]
        public async Task<ActionResult<string>> RemoveFromWishlist(int id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return Unauthorized();
            }
            Guid userIdguid = Guid.Parse(userId);
            var status = await _wishlistServices.RemoveWishlist(id, userIdguid);
            if (status is null) return NotFound();
            return Ok(status);
        }
    }
}
