using chronovault_api.DTOs.Request;
using chronovault_api.DTOs.Response;
using chronovault_api.Models.Enums;

namespace chronovault_api.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDTO?> GetByIdAsync(int id);
        Task<OrderResponseDTO?> GetByOrderNumberAsync(string orderNumber);
        Task<IEnumerable<OrderResponseDTO>> GetAllAsync();
        Task<OrderResponseDTO?> CreateAsync(OrderCreateDTO orderCreateDTO);
        Task<OrderResponseDTO?> UpdateAsync(int id, OrderUpdateDTO orderUpdateDTO);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);

        Task<IEnumerable<OrderResponseDTO>> GetOrdersByUserIdAsync(int userId);
        Task<OrderResponseDTO?> GetLastOrderByUserIdAsync(int userId);

        Task<IEnumerable<OrderResponseDTO>> GetOrdersByStatusAsync(OrderStatus status);
        Task<IEnumerable<OrderResponseDTO>> GetPendingOrdersAsync();
        Task<IEnumerable<OrderResponseDTO>> GetProcessingOrdersAsync();
        Task<IEnumerable<OrderResponseDTO>> GetShippedOrdersAsync();
        Task<IEnumerable<OrderResponseDTO>> GetDeliveredOrdersAsync();

        Task<IEnumerable<OrderResponseDTO>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<OrderResponseDTO>> GetTodayOrdersAsync();
        Task<IEnumerable<OrderResponseDTO>> GetOrdersThisMonthAsync();

        Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus);
        Task<bool> UpdatePaymentStatusAsync(int orderId, PaymentStatus paymentStatus, DateTime? paymentDate = null);

        Task<(IEnumerable<OrderResponseDTO> Orders, int TotalCount)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            OrderStatus? status = null,
            int? userId = null,
            DateTime? startDate = null,
            DateTime? endDate = null);

        Task<decimal> GetTotalSalesAsync(DateTime? startDate = null, DateTime? endDate = null);
        Task<int> GetTotalOrdersCountAsync(DateTime? startDate = null, DateTime? endDate = null);
        Task<IEnumerable<OrderResponseDTO>> GetTopOrdersByValueAsync(int count = 10);
    }
}