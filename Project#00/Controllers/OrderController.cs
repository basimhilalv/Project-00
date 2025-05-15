using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_00.Models;
using Project_00.Services.OrderService;
using System.Security.Claims;

namespace Project_00.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;
        public OrderController (IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }
        [Authorize]
        [HttpGet("getOrders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return Unauthorized();
            }
            Guid userIdguid = Guid.Parse(userId);
            var orders = await _orderServices.GetOrders(userIdguid);
            if (orders is null) return NotFound();
            return Ok(orders);
        }
        [Authorize]
        [HttpGet("UserDash")]
        public async Task<ActionResult<Dashboard>> GetUserDash()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null) return Unauthorized();
            Guid userIdguid = Guid.Parse(userId);
            var dash = await _orderServices.GetUserDashboard(userIdguid);
            if (dash is null) return NotFound();
            return Ok(dash);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("AdminDash")]
        public async Task<ActionResult<Dashboard>> GetAdminDash()
        {
            var dash = await _orderServices.GetAdminDashboard();
            if (dash is null) return NotFound();
            return Ok(dash);
        }
    }
}
