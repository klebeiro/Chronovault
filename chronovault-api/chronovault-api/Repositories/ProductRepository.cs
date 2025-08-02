using Microsoft.EntityFrameworkCore;
using chronovault_api.Models;
using chronovault_api.Repositories.Interfaces;
using chronovault_api.Infra.Data;
using AutoMapper;

namespace chronovault_api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ChronovaultDbContext _context;

        public ProductRepository(ChronovaultDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .ToListAsync();
        }

        public async Task<Product> CreateAsync(Product product)
        {
            var createdProduct = _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetAllProductsFromOrderItems(List<int> productIds)
        {
            var products = await _context.Products
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            return products;
        }
    }
}