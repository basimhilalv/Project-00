using Microsoft.EntityFrameworkCore;
using Project_00.Data;
using Project_00.Models;

namespace Project_00.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly UserDbContext _context;
        public OrderServices(UserDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Order>> GetOrders(Guid userId)
        {
            var orders = await _context.Orders
                .Include(p=>p.Products)
                .ThenInclude(p=>p.Product)
                .Where(o => o.UserId == userId).ToListAsync();
            if (orders is null) return null;
            return orders;
        }
    }
}
