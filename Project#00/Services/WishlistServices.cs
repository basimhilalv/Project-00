using Microsoft.EntityFrameworkCore;
using Project_00.Data;
using Project_00.Models;

namespace Project_00.Services
{
    public class WishlistServices : IWishlistServices
    {
        private readonly UserDbContext _context;
        public WishlistServices(UserDbContext context)
        {
            _context = context;
        }
        public async Task<string> AddToWishlist(WishlistDto wishlist, Guid userId)
        {
            var product = await _context.Products.FindAsync(wishlist.ProductId);
            if (product is null) return null;
            if(await _context.Wishlists.AnyAsync(w => w.ProductId == wishlist.ProductId && w.UserId == userId))
            {
                return "Item is already in wishlist";
            }
            else
            {
                var wishItem = new Wishlist
                {
                    UserId = userId,
                    ProductId = wishlist.ProductId
                };
                _context.Wishlists.Add(wishItem);
            }
            await _context.SaveChangesAsync();
            return "Item added to wishlist";
        }

        public async Task<IEnumerable<Wishlist>> GetWishlist(Guid userId)
        {
            var items = await _context.Wishlists
                .Include(w => w.Product)
                .Where(w => w.UserId == userId)
                .ToListAsync();
            if (items is null) return null;
            return items;
        }

        public async Task<string> RemoveWishlist(int id, Guid userId)
        {
            var item = await _context.Wishlists.FirstOrDefaultAsync(w => w.Id == id && w.UserId == userId);
            if (item is null) return null;
            _context.Wishlists.Remove(item);
            await _context.SaveChangesAsync();
            return "Item deleted from wishlist";
        }
    }
}
