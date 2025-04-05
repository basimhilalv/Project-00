using Microsoft.EntityFrameworkCore;
using Project_00.Data;
using Project_00.Models;

namespace Project_00.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly UserDbContext _context;
        public PaymentServices(UserDbContext context)
        {
            _context = context;
        }
        public async Task<PaymentCart> MakePaymentCart(Guid userID)
        {
            var cartItems = await _context.CartItems.Include(c => c.Product).Where(c => c.UserId == userID).ToListAsync();
            if (cartItems is null) return null;
            var totalAmount = cartItems.Sum(c => c.Quantity * c.Product.Price);
            var payment = new PaymentCart();
            payment.Amount = totalAmount;
            payment.UserId = userID;
            payment.PaymentDate = DateTime.UtcNow;
            payment.Products = cartItems;
            _context.PaymentCarts.Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<PaymentProduct> MakePaymentProduct(PaymentProductDto payment, Guid userID)
        {
            var product = await _context.Products.FindAsync(payment.ProductId);
            if (product is null) return null;
            var totalAmount = payment.Quantity * product.Price;
            var payments = new PaymentProduct();
            payments.ProductId = product.Id;
            payments.PaymentDate = DateTime.UtcNow;
            payments.Quantity = payment.Quantity;
            payments.Amount = totalAmount;
            payments.UserId = userID;
            _context.PaymentProducts.Add(payments);
            await _context.SaveChangesAsync();
            return payments;
        }
    }
}
