using Project_00.Models;

namespace Project_00.Services
{
    public interface IOrderServices
    {
        public Task<IEnumerable<Order>> GetOrders(Guid userId);
    }
}
