using chronovault_api.Models;
using chronovault_api.Models.Enums;

namespace chronovault_api.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(int id);
        Task<List<Order>> GetAllByUserIdAsync(int id);
        Task<Order> CreateAsync(Order order);
    }
}