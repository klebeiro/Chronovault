using Microsoft.EntityFrameworkCore;
using chronovault_api.Models;
using chronovault_api.Models.Enums;
using chronovault_api.Repositories.Interfaces;
using chronovault_api.Infra.Data;

namespace chronovault_api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ChronovaultDbContext _context;

        public OrderRepository(ChronovaultDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order?> GetByOrderNumberAsync(string orderNumber)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<Order> CreateAsync(Order order)
        {
            order.CreatedAt = DateTime.Now;
            order.OrderNumber = GenerateOrderNumber();
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            order.UpdatedAt = DateTime.Now;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Orders.AnyAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<Order?> GetLastOrderByUserIdAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.Status == status)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetPendingOrdersAsync()
        {
            return await GetOrdersByStatusAsync(OrderStatus.Pending);
        }

        public async Task<IEnumerable<Order>> GetProcessingOrdersAsync()
        {
            return await GetOrdersByStatusAsync(OrderStatus.Processing);
        }

        public async Task<IEnumerable<Order>> GetShippedOrdersAsync()
        {
            return await GetOrdersByStatusAsync(OrderStatus.Shipped);
        }

        public async Task<IEnumerable<Order>> GetDeliveredOrdersAsync()
        {
            return await GetOrdersByStatusAsync(OrderStatus.Delivered);
        }

        public async Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.CreatedAt >= startDate && o.CreatedAt <= endDate)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetTodayOrdersAsync()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            return await GetOrdersByDateRangeAsync(today, tomorrow);
        }

        public async Task<IEnumerable<Order>> GetOrdersThisMonthAsync()
        {
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            return await GetOrdersByDateRangeAsync(firstDayOfMonth, lastDayOfMonth);
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;

            order.Status = newStatus;
            order.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatePaymentStatusAsync(int orderId, PaymentStatus paymentStatus, DateTime? paymentDate = null)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;

            order.PaymentStatus = paymentStatus;
            if (paymentDate.HasValue)
                order.PaymentDate = paymentDate.Value;
            order.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<(IEnumerable<Order> Orders, int TotalCount)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            OrderStatus? status = null,
            int? userId = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            var query = _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .AsQueryable();

            if (status.HasValue)
                query = query.Where(o => o.Status == status.Value);

            if (userId.HasValue)
                query = query.Where(o => o.UserId == userId.Value);

            if (startDate.HasValue)
                query = query.Where(o => o.CreatedAt >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(o => o.CreatedAt <= endDate.Value);

            var totalCount = await query.CountAsync();
            var orders = await query
                .OrderByDescending(o => o.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (orders, totalCount);
        }

        public async Task<decimal> GetTotalSalesAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _context.Orders.AsQueryable();

            if (startDate.HasValue)
                query = query.Where(o => o.CreatedAt >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(o => o.CreatedAt <= endDate.Value);

            return await query
                .Where(o => o.PaymentStatus == PaymentStatus.Approved)
                .SumAsync(o => o.TotalAmount);
        }

        public async Task<int> GetTotalOrdersCountAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _context.Orders.AsQueryable();

            if (startDate.HasValue)
                query = query.Where(o => o.CreatedAt >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(o => o.CreatedAt <= endDate.Value);

            return await query.CountAsync();
        }

        public async Task<IEnumerable<Order>> GetTopOrdersByValueAsync(int count = 10)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.TotalAmount)
                .Take(count)
                .ToListAsync();
        }

        private string GenerateOrderNumber()
        {
            return $"ORD-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
        }
    }
}