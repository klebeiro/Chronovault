using chronovault_api.Models;
using chronovault_api.Models.Enums;

namespace chronovault_api.Repositories.Interfaces
{
    public interface IProductRepository
    {
        // Operações básicas CRUD
        Task<Product> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetActiveProductsAsync();
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);

        // Consultas específicas - usando enums
        Task<IEnumerable<Product>> GetByBrandAsync(string brand);
        Task<IEnumerable<Product>> GetByCategoryAsync(WatchCategory category);
        Task<IEnumerable<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<Product>> GetByGenderAsync(ProductGender gender);
        Task<IEnumerable<Product>> GetByMovementTypeAsync(MovementType movementType);

        // Busca e filtros
        Task<IEnumerable<Product>> SearchAsync(string searchTerm);
        Task<(IEnumerable<Product> Products, int TotalCount)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            string brand = null,
            WatchCategory? category = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            ProductGender? gender = null);

        // Operações de estoque
        Task<bool> UpdateStockAsync(int productId, int quantity);
        Task<bool> IsInStockAsync(int productId);
        Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold = 5);

        // Produtos em destaque/promoção
        Task<IEnumerable<Product>> GetFeaturedProductsAsync();
        Task<IEnumerable<Product>> GetOnSaleProductsAsync();
        Task<IEnumerable<Product>> GetNewArrivalsAsync(int days = 30);

        // Produtos relacionados
        Task<IEnumerable<Product>> GetRelatedProductsAsync(int productId, int count = 4);
    }
}