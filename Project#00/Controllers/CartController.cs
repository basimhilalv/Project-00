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
    public class CartController : ControllerBase
    {
        private readonly ICartServices _cartServices;
        public CartController(ICartServices cartServices)   
        {
            _cartServices = cartServices;
        }
        [Authorize]
        [HttpGet("GetCart")]
        public async Task<ActionResult<IEnumerable<Cart>>> getCartItems()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return Unauthorized();
            }
            Guid userIdguid = Guid.Parse(userId);
            var items = await _cartServices.GetCartItems(userIdguid);
            if (items is null) return NotFound();
            return Ok(items);
        }

        [Authorize]
        [HttpPost("AddToCart")]
        public async Task<ActionResult<string>> AddtoCart(CartDto cart)
        {
            var status = await _cartServices.AddToCart(cart);
            if (status is null) return BadRequest();
            return Ok(status);
        }
        [Authorize]
        [HttpPut("UpdateCart")]
        public async Task<ActionResult<Cart>> UpdateCart(int id, CartDto cart)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return Unauthorized();
            }
            Guid userIdguid = Guid.Parse(userId);
            var status = await _cartServices.UpdateCart(id,cart,userIdguid);
            if (status is null) return BadRequest();
            return Ok(status);
        }

        [Authorize]
        [HttpDelete("DeleteCart")]
        public async Task<ActionResult> RemoveFromCart(int id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return Unauthorized();
            }
            Guid userIdguid = Guid.Parse(userId);
            var status = await _cartServices.RemoveFromCart(id, userIdguid);
            if (status is null) return NotFound();
            return NoContent();
        }
    }
}
