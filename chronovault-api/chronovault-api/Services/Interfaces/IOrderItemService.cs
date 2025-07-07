using chronovault_api.DTOs.Request;
using chronovault_api.DTOs.Response;

namespace chronovault_api.Services.Interfaces
{
    public interface IOrderItemService
    {
        Task<OrderItemResponseDTO?> GetByIdAsync(int id);
        Task<IEnumerable<OrderItemResponseDTO>> GetByOrderIdAsync(int orderId);
    }
}