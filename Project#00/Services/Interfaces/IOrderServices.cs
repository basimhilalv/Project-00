using Project_00.Models;

namespace Project_00.Services.Interfaces
{
    public interface IOrderServices
    {
        public Task<IEnumerable<Order>> GetOrders(Guid userId);
        public Task<Dashboard> GetAdminDashboard();
        public Task<Dashboard> GetUserDashboard(Guid userId);
    }
}
