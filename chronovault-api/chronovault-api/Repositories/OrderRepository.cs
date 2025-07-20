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

        public async Task<List<Order>> GetAllByUserIdAsync(int id)
        {
            return await _context.Orders
                .Where(o => o.UserId == id)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<Order> CreateAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}