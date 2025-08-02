using chronovault_api.Models;
using chronovault_api.Models.Enums;

namespace chronovault_api.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> CreateAsync(Product product);
        Task<List<Product>> GetAllProductsFromOrderItems(List<int> productIds);
    }
}