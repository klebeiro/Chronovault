using chronovault_api.Models;
using chronovault_api.Models.Enums;

namespace chronovault_api.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        // Operações básicas CRUD
        Task<Order?> GetByIdAsync(int id);
        Task<Order?> GetByOrderNumberAsync(string orderNumber);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> CreateAsync(Order order);
        Task<Order> UpdateAsync(Order order);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);

        // Consultas por usuário
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
        Task<Order?> GetLastOrderByUserIdAsync(int userId);

        // Consultas por status - usando enums
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status);
        Task<IEnumerable<Order>> GetPendingOrdersAsync();
        Task<IEnumerable<Order>> GetProcessingOrdersAsync();
        Task<IEnumerable<Order>> GetShippedOrdersAsync();
        Task<IEnumerable<Order>> GetDeliveredOrdersAsync();

        // Consultas por data
        Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Order>> GetTodayOrdersAsync();
        Task<IEnumerable<Order>> GetOrdersThisMonthAsync();

        // Operações de status 
        Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus);
        Task<bool> UpdatePaymentStatusAsync(int orderId, PaymentStatus paymentStatus, DateTime? paymentDate = null);

        // Paginação
        Task<(IEnumerable<Order> Orders, int TotalCount)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            OrderStatus? status = null,
            int? userId = null,
            DateTime? startDate = null,
            DateTime? endDate = null);

        // Relatórios e estatísticas
        Task<decimal> GetTotalSalesAsync(DateTime? startDate = null, DateTime? endDate = null);
        Task<int> GetTotalOrdersCountAsync(DateTime? startDate = null, DateTime? endDate = null);
        Task<IEnumerable<Order>> GetTopOrdersByValueAsync(int count = 10);
    }
}