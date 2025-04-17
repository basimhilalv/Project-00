using Project_00.Dtos;
using Project_00.Models;

namespace Project_00.Services.Interfaces
{
    public interface ICartServices
    {
        public Task<IEnumerable<Cart>> GetCartItems(Guid userId);
        public Task<string> AddToCart(CartDto cart);
        public Task<Cart> RemoveFromCart(int id, Guid userId);
        public Task<Cart> UpdateCart(int id, CartDto cart, Guid userId);
    }
}
