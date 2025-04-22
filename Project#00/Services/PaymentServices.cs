using Microsoft.EntityFrameworkCore;
using Project_00.Data;
using Project_00.Dtos;
using Project_00.Migrations;
using Project_00.Models;
using Project_00.Services.Interfaces;

namespace Project_00.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly Context _context;
        public PaymentServices(Context context)
        {
            _context = context;
        }
        public async Task<PaymentCart> MakePaymentCart(Guid userID)
        {
            try
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

                List<ProductPurchase> products = new List<ProductPurchase>();
                foreach(var cart in cartItems)
                {
                    var productPurhcase = new ProductPurchase
                    {
                        ProductId = cart.ProductId,
                        Product = cart.Product,
                        Quantity = cart.Quantity,
                        UserId = cart.UserId,
                        User = cart.User
                    };
                    products.Add(productPurhcase);
                    _context.ProductPurachases.Add(productPurhcase);
                }

                var order = new Order();
                order.Amount = totalAmount;
                order.UserId = userID;
                order.OrderDate = DateTime.UtcNow;
                order.PaymentMode = "Cart";
                order.PaymentId = payment.Id;
                order.Quantity = cartItems.Sum(c => c.Quantity);
                order.Products = products;
                _context.Orders.Add(order);

                _context.CartItems.RemoveRange(cartItems);

                await _context.SaveChangesAsync();
                return payment;
            }catch(Exception ex)
            {
                throw new Exception("Error occured while fetching data", ex);
            }
        }

        public async Task<PaymentProduct> MakePaymentProduct(PaymentProductDto payment, Guid userID)
        {
            try
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

                var productPurchase = new List<ProductPurchase>();
                var products = new ProductPurchase();
                products.ProductId = product.Id;
                products.Product = product;
                products.Quantity = payments.Quantity;
                products.UserId = userID;
                productPurchase.Add(products);
                _context.ProductPurachases.Add(products);

                var order = new Order();
                order.Amount = totalAmount;
                order.UserId = userID;
                order.OrderDate = DateTime.UtcNow;
                order.PaymentMode = "Product";
                order.PaymentId = payments.Id;
                order.Quantity = payments.Quantity;
                order.Products = productPurchase;
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return payments;
            }catch(Exception ex)
            {
                throw new Exception("Error ocured while fetching data", ex);
            }
        }
    }
}
