using Project_00.Dtos;
using Project_00.Models;

namespace Project_00.Services.Interfaces
{
    public interface IWishlistServices
    {
        public Task<IEnumerable<Wishlist>> GetWishlist(Guid userId);
        public Task<string> AddToWishlist(WishlistDto wishlist, Guid userId);
        public Task<string> RemoveWishlist(int id, Guid userId);
    }
}
