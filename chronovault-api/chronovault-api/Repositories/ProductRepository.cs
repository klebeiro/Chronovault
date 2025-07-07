using Microsoft.EntityFrameworkCore;
using chronovault_api.Models;
using chronovault_api.Models.Enums;
using chronovault_api.Repositories.Interfaces;
using chronovault_api.Infra.Data;

namespace chronovault_api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ChronovaultDbContext _context;

        public ProductRepository(ChronovaultDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Images)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetActiveProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Images)
                .Where(p => p.IsActive)
                .ToListAsync();
        }

        public async Task<Product> CreateAsync(Product product)
        {
            product.CreatedAt = DateTime.Now;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            product.UpdatedAt = DateTime.Now;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetByBrandAsync(string brand)
        {
            return await _context.Products
                .Include(p => p.Images)
                .Where(p => p.Brand == brand && p.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(WatchCategory category)
        {
            return await _context.Products
                .Include(p => p.Images)
                .Where(p => p.Category == category && p.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _context.Products
                .Include(p => p.Images)
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice && p.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByGenderAsync(ProductGender gender)
        {
            return await _context.Products
                .Include(p => p.Images)
                .Where(p => p.Gender == gender && p.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByMovementTypeAsync(MovementType movementType)
        {
            return await _context.Products
                .Include(p => p.Images)
                .Where(p => p.MovementType == movementType && p.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchAsync(string searchTerm)
        {
            return await _context.Products
                .Include(p => p.Images)
                .Where(p => (p.Model.Contains(searchTerm) ||
                           p.Brand.Contains(searchTerm) ||
                           p.Description.Contains(searchTerm)) && p.IsActive)
                .ToListAsync();
        }

        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            string brand = null,
            WatchCategory? category = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            ProductGender? gender = null)
        {
            var query = _context.Products
                .Include(p => p.Images)
                .Where(p => p.IsActive);

            if (!string.IsNullOrEmpty(brand))
                query = query.Where(p => p.Brand == brand);

            if (category.HasValue)
                query = query.Where(p => p.Category == category.Value);

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            if (gender.HasValue)
                query = query.Where(p => p.Gender == gender.Value);

            var totalCount = await query.CountAsync();
            var products = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }

        public async Task<bool> UpdateStockAsync(int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return false;

            product.StockQuantity = quantity;
            product.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsInStockAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            return product != null && product.StockQuantity > 0;
        }

        public async Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold = 5)
        {
            return await _context.Products
                .Include(p => p.Images)
                .Where(p => p.StockQuantity <= threshold && p.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetFeaturedProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Images)
                .Where(p => p.IsFeatured && p.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetOnSaleProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Images)
                .Where(p => p.IsOnSale && p.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetNewArrivalsAsync(int days = 30)
        {
            var cutoffDate = DateTime.Now.AddDays(-days);
            return await _context.Products
                .Include(p => p.Images)
                .Where(p => p.CreatedAt >= cutoffDate && p.IsActive)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetRelatedProductsAsync(int productId, int count = 4)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return new List<Product>();

            return await _context.Products
                .Include(p => p.Images)
                .Where(p => p.Id != productId &&
                           (p.Brand == product.Brand || p.Category == product.Category) &&
                           p.IsActive)
                .Take(count)
                .ToListAsync();
        }
    }
}