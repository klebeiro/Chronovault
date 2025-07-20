using chronovault_api.DTOs.Request;
using chronovault_api.DTOs.Response;

namespace chronovault_api.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDetailsDTO?> GetByIdAsync(int id);
        Task<List<OrderResponseDTO>> GetAllByUserIdAsync(int id);
        Task<OrderResponseDetailsDTO?> CreateAsync(OrderCreateDTO orderCreateDTO, int userId);
    }
}