using Microsoft.EntityFrameworkCore;
using Project_00.Data;
using Project_00.Dtos;
using Project_00.Models;
using System.Security.Claims;

namespace Project_00.Services.CartService
{
    public class CartServices : ICartServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Context _context;
        public CartServices(IHttpContextAccessor httpContextAccessor, Context context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<string> AddToCart(CartDto cart)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if(userId is null)
                {
                    return null;
                }
                Guid userIdguid = Guid.Parse(userId);

                var product = await _context.Products.FindAsync(cart.ProductId);
                if (product is null) return null;

                var existingItem = await _context.CartItems.FirstOrDefaultAsync(c => c.UserId == userIdguid && c.ProductId == cart.ProductId);
                if(existingItem != null)
                {
                    existingItem.Quantity += cart.Quantity;
                    _context.Update(existingItem);
                }
                else
                {
                    var cartItem = new Cart
                    {
                        UserId = userIdguid,
                        ProductId = cart.ProductId,
                        Quantity = cart.Quantity
                    };
                    _context.CartItems.Add(cartItem);
                }
                await _context.SaveChangesAsync();
                return "Item Added to cart";

            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while fetching data", ex);
            }
        }

        public async Task<IEnumerable<Cart>> GetCartItems(Guid userId)
        {
            try
            {
                var cartItems = await _context.CartItems
                    .Include(c => c.Product)
                    .Where(c => c.UserId == userId)
                    .ToListAsync();
                if (cartItems is null) return null;
                return cartItems;

            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while fetching data", ex);
            }
        }

        public async Task<Cart> RemoveFromCart(int id, Guid userId)
        {
            try
            {
                var cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
                if (cartItem is null) return null;
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
                return cartItem;

            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while fetching data", ex);
            }
        }

        public async Task<Cart> UpdateCart(int id, CartDto cart, Guid userId)
        {
            try
            {
                var cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
                if (cartItem is null) return null;
                cartItem.ProductId = cart.ProductId;
                cartItem.Quantity = cart.Quantity;
                await _context.SaveChangesAsync();
                return cartItem;

            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while fetching data", ex);
            }
        }
    }
}
