using AutoMapper;
using chronovault_api.DTOs.Request;
using chronovault_api.DTOs.Response;
using chronovault_api.Models;
using chronovault_api.Models.Enums;
using chronovault_api.Repositories.Interfaces;
using chronovault_api.Services.Interfaces;

namespace chronovault_api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderResponseDTO?> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return order != null ? _mapper.Map<OrderResponseDTO>(order) : null;
        }

        public async Task<OrderResponseDTO?> GetByOrderNumberAsync(string orderNumber)
        {
            var order = await _orderRepository.GetByOrderNumberAsync(orderNumber);
            return order != null ? _mapper.Map<OrderResponseDTO>(order) : null;
        }

        public async Task<IEnumerable<OrderResponseDTO>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderResponseDTO>>(orders);
        }

        public async Task<OrderResponseDTO?> CreateAsync(OrderCreateDTO orderCreateDTO)
        {
            var order = _mapper.Map<Order>(orderCreateDTO);
            order.CreatedAt = DateTime.UtcNow;
            order.Status = OrderStatus.Pending;
            order.PaymentStatus = PaymentStatus.Pending;
            order.OrderNumber = await GenerateOrderNumberAsync();

            // Calcule o valor total do pedido
            order.TotalAmount = order.OrderItems.Sum(item => (item.Price * item.Quantity) - item.DiscountAmount)
                                + order.ShippingCost - order.DiscountAmount;

            var created = await _orderRepository.CreateAsync(order);
            return created != null ? _mapper.Map<OrderResponseDTO>(created) : null;
        }

        public async Task<OrderResponseDTO?> UpdateAsync(int id, OrderUpdateDTO orderUpdateDTO)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null) return null;

            _mapper.Map(orderUpdateDTO, order);
            order.UpdatedAt = DateTime.UtcNow;

            // Recalcule o valor total do pedido
            order.TotalAmount = order.OrderItems.Sum(item => (item.Price * item.Quantity) - item.DiscountAmount)
                                + order.ShippingCost - order.DiscountAmount;

            var updated = await _orderRepository.UpdateAsync(order);
            return updated != null ? _mapper.Map<OrderResponseDTO>(updated) : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _orderRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _orderRepository.ExistsAsync(id);
        }

        public async Task<IEnumerable<OrderResponseDTO>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<OrderResponseDTO>>(orders);
        }

        public async Task<OrderResponseDTO?> GetLastOrderByUserIdAsync(int userId)
        {
            var order = await _orderRepository.GetLastOrderByUserIdAsync(userId);
            return order != null ? _mapper.Map<OrderResponseDTO>(order) : null;
        }

        public async Task<IEnumerable<OrderResponseDTO>> GetOrdersByStatusAsync(OrderStatus status)
        {
            var orders = await _orderRepository.GetOrdersByStatusAsync(status);
            return _mapper.Map<IEnumerable<OrderResponseDTO>>(orders);
        }

        public async Task<IEnumerable<OrderResponseDTO>> GetPendingOrdersAsync()
        {
            var orders = await _orderRepository.GetPendingOrdersAsync();
            return _mapper.Map<IEnumerable<OrderResponseDTO>>(orders);
        }

        public async Task<IEnumerable<OrderResponseDTO>> GetProcessingOrdersAsync()
        {
            var orders = await _orderRepository.GetProcessingOrdersAsync();
            return _mapper.Map<IEnumerable<OrderResponseDTO>>(orders);
        }

        public async Task<IEnumerable<OrderResponseDTO>> GetShippedOrdersAsync()
        {
            var orders = await _orderRepository.GetShippedOrdersAsync();
            return _mapper.Map<IEnumerable<OrderResponseDTO>>(orders);
        }

        public async Task<IEnumerable<OrderResponseDTO>> GetDeliveredOrdersAsync()
        {
            var orders = await _orderRepository.GetDeliveredOrdersAsync();
            return _mapper.Map<IEnumerable<OrderResponseDTO>>(orders);
        }

        public async Task<IEnumerable<OrderResponseDTO>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var orders = await _orderRepository.GetOrdersByDateRangeAsync(startDate, endDate);
            return _mapper.Map<IEnumerable<OrderResponseDTO>>(orders);
        }

        public async Task<IEnumerable<OrderResponseDTO>> GetTodayOrdersAsync()
        {
            var orders = await _orderRepository.GetTodayOrdersAsync();
            return _mapper.Map<IEnumerable<OrderResponseDTO>>(orders);
        }

        public async Task<IEnumerable<OrderResponseDTO>> GetOrdersThisMonthAsync()
        {
            var orders = await _orderRepository.GetOrdersThisMonthAsync();
            return _mapper.Map<IEnumerable<OrderResponseDTO>>(orders);
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus)
        {
            return await _orderRepository.UpdateOrderStatusAsync(orderId, newStatus);
        }

        public async Task<bool> UpdatePaymentStatusAsync(int orderId, PaymentStatus paymentStatus, DateTime? paymentDate = null)
        {
            return await _orderRepository.UpdatePaymentStatusAsync(orderId, paymentStatus, paymentDate);
        }

        public async Task<(IEnumerable<OrderResponseDTO> Orders, int TotalCount)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            OrderStatus? status = null,
            int? userId = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            var (orders, totalCount) = await _orderRepository.GetPagedAsync(
                pageNumber, pageSize, status, userId, startDate, endDate);

            return (_mapper.Map<IEnumerable<OrderResponseDTO>>(orders), totalCount);
        }

        public async Task<decimal> GetTotalSalesAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            return await _orderRepository.GetTotalSalesAsync(startDate, endDate);
        }

        public async Task<int> GetTotalOrdersCountAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            return await _orderRepository.GetTotalOrdersCountAsync(startDate, endDate);
        }

        public async Task<IEnumerable<OrderResponseDTO>> GetTopOrdersByValueAsync(int count = 10)
        {
            var orders = await _orderRepository.GetTopOrdersByValueAsync(count);
            return _mapper.Map<IEnumerable<OrderResponseDTO>>(orders);
        }

        // Gera um número de pedido único (ajuste conforme sua regra de negócio)
        private async Task<string> GenerateOrderNumberAsync()
        {
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var random = new Random().Next(1000, 9999);
            return $"ORD-{timestamp}-{random}";
        }
    }
}