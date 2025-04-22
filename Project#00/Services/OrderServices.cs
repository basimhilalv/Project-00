using Microsoft.EntityFrameworkCore;
using Project_00.Data;
using Project_00.Models;
using Project_00.Services.Interfaces;

namespace Project_00.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly Context _context;
        public OrderServices(Context context)
        {
            _context = context;
        }

        public async Task<Dashboard> GetAdminDashboard()
        {
            try
            {
                var orders = await _context.Orders.ToListAsync();
                if (orders is null) return null;
                var dash = new Dashboard();
                dash.totalRevenue = orders.Sum(t => t.Amount);
                dash.totalOrders = orders.Count();
                dash.totalProducts = _context.ProductPurachases.Count();
                return dash;

            }catch(Exception ex)
            {
                throw new Exception("Error occured while fetching data", ex);
            }
        }

        public async Task<IEnumerable<Order>> GetOrders(Guid userId)
        {
            try
            {
                var orders = await _context.Orders
                    .Include(p=>p.Products)
                    .ThenInclude(p=>p.Product)
                    .Where(o => o.UserId == userId).ToListAsync();
                if (orders is null) return null;
                return orders;

            }catch(Exception ex)
            {
                throw new Exception("Error occured while fetching data", ex);
            }
        }

        public async Task<Dashboard> GetUserDashboard(Guid userId)
        {
            try
            {
                var orders = await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
                if (orders is null) return null;
                var dash = new Dashboard();
                dash.totalProducts = _context.ProductPurachases.Where(p => p.UserId == userId).Count();
                dash.totalRevenue = orders.Sum(t => t.Amount);
                dash.totalOrders = orders.Count();
                return dash;

            }catch(Exception ex)
            {
                throw new Exception("Error occured while fetching data", ex);
            }
        }
    }
}
