using chronovault_api.DTOs.Request;
using chronovault_api.DTOs.Response;
using chronovault_api.Models.Enums;

namespace chronovault_api.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponseDTO?> GetByIdAsync(int id);
        Task<IEnumerable<ProductResponseDTO>> GetAllAsync();
        Task<IEnumerable<ProductResponseDTO>> GetActiveProductsAsync();
        Task<ProductResponseDTO?> CreateAsync(ProductCreateDTO productCreateDTO);
        Task<ProductResponseDTO?> UpdateAsync(int id, ProductUpdateDTO productUpdateDTO);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);

        Task<IEnumerable<ProductResponseDTO>> GetByBrandAsync(string brand);
        Task<IEnumerable<ProductResponseDTO>> GetByCategoryAsync(WatchCategory category);
        Task<IEnumerable<ProductResponseDTO>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<ProductResponseDTO>> GetByGenderAsync(ProductGender gender);
        Task<IEnumerable<ProductResponseDTO>> GetByMovementTypeAsync(MovementType movementType);

        Task<IEnumerable<ProductResponseDTO>> SearchAsync(string searchTerm);
        Task<(IEnumerable<ProductResponseDTO> Products, int TotalCount)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            string brand = null,
            WatchCategory? category = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            ProductGender? gender = null);

        Task<bool> UpdateStockAsync(int productId, int quantity);
        Task<bool> IsInStockAsync(int productId);
        Task<IEnumerable<ProductResponseDTO>> GetLowStockProductsAsync(int threshold = 5);

        Task<IEnumerable<ProductResponseDTO>> GetFeaturedProductsAsync();
        Task<IEnumerable<ProductResponseDTO>> GetOnSaleProductsAsync();
        Task<IEnumerable<ProductResponseDTO>> GetNewArrivalsAsync(int days = 30);

        Task<IEnumerable<ProductResponseDTO>> GetRelatedProductsAsync(int productId, int count = 4);
    }
}