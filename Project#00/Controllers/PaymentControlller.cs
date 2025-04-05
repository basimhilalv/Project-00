using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Project_00.Models;
using Project_00.Services;
using System.Security.Claims;

namespace Project_00.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentControlller : ControllerBase
    {
        private readonly IPaymentServices _paymentServices;
        public PaymentControlller(IPaymentServices paymentServices)
        {
            _paymentServices = paymentServices;
        }
        [Authorize]
        [HttpPost("CartPayment")]
        public async Task<ActionResult<PaymentCart>> makepaymentcart()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return Unauthorized();
            }
            Guid userIdguid = Guid.Parse(userId);
            var payment = await _paymentServices.MakePaymentCart(userIdguid);
            if (payment is null) return BadRequest();
            return Ok(payment);
        }

        [Authorize]
        [HttpPost("ProductPayment")]
        public async Task<ActionResult<PaymentProduct>> makepaymentproduct(PaymentProductDto request)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return Unauthorized();
            }
            Guid userIdguid = Guid.Parse(userId);
            var payment = await _paymentServices.MakePaymentProduct(request, userIdguid);
            if (payment is null) return BadRequest();
            return Ok(payment);
        }
    }
}
