using Microsoft.EntityFrameworkCore;
using chronovault_api.Models;
using chronovault_api.Repositories.Interfaces;
using chronovault_api.Infra.Data;

namespace chronovault_api.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ChronovaultDbContext _context;

        public OrderItemRepository(ChronovaultDbContext context)
        {
            _context = context;
        }

        public async Task<OrderItem?> GetByIdAsync(int id)
        {
            return await _context.OrderItems
                .Include(oi => oi.Product)
                .Include(oi => oi.Order)
                .FirstOrDefaultAsync(oi => oi.Id == id);
        }

        public async Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId)
        {
            return await _context.OrderItems
                .Include(oi => oi.Product)
                .Where(oi => oi.OrderId == orderId)
                .ToListAsync();
        }
    }
}