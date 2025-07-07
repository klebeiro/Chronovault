using AutoMapper;
using chronovault_api.DTOs.Request;
using chronovault_api.DTOs.Response;
using chronovault_api.Models;
using chronovault_api.Models.Enums;
using chronovault_api.Repositories.Interfaces;
using chronovault_api.Services.Interfaces;

namespace chronovault_api.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductResponseDTO?> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product != null ? _mapper.Map<ProductResponseDTO>(product) : null;
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductResponseDTO>>(products);
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetActiveProductsAsync()
        {
            var products = await _productRepository.GetActiveProductsAsync();
            return _mapper.Map<IEnumerable<ProductResponseDTO>>(products);
        }

        public async Task<ProductResponseDTO?> CreateAsync(ProductCreateDTO productCreateDTO)
        {
            var product = _mapper.Map<Product>(productCreateDTO);
            product.CreatedAt = DateTime.UtcNow;
            var created = await _productRepository.CreateAsync(product);
            return created != null ? _mapper.Map<ProductResponseDTO>(created) : null;
        }

        public async Task<ProductResponseDTO?> UpdateAsync(int id, ProductUpdateDTO productUpdateDTO)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return null;

            _mapper.Map(productUpdateDTO, product);
            product.UpdatedAt = DateTime.UtcNow;
            var updated = await _productRepository.UpdateAsync(product);
            return updated != null ? _mapper.Map<ProductResponseDTO>(updated) : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _productRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _productRepository.ExistsAsync(id);
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetByBrandAsync(string brand)
        {
            var products = await _productRepository.GetByBrandAsync(brand);
            return _mapper.Map<IEnumerable<ProductResponseDTO>>(products);
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetByCategoryAsync(WatchCategory category)
        {
            var products = await _productRepository.GetByCategoryAsync(category);
            return _mapper.Map<IEnumerable<ProductResponseDTO>>(products);
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var products = await _productRepository.GetByPriceRangeAsync(minPrice, maxPrice);
            return _mapper.Map<IEnumerable<ProductResponseDTO>>(products);
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetByGenderAsync(ProductGender gender)
        {
            var products = await _productRepository.GetByGenderAsync(gender);
            return _mapper.Map<IEnumerable<ProductResponseDTO>>(products);
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetByMovementTypeAsync(MovementType movementType)
        {
            var products = await _productRepository.GetByMovementTypeAsync(movementType);
            return _mapper.Map<IEnumerable<ProductResponseDTO>>(products);
        }

        public async Task<IEnumerable<ProductResponseDTO>> SearchAsync(string searchTerm)
        {
            var products = await _productRepository.SearchAsync(searchTerm);
            return _mapper.Map<IEnumerable<ProductResponseDTO>>(products);
        }

        public async Task<(IEnumerable<ProductResponseDTO> Products, int TotalCount)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            string brand = null,
            WatchCategory? category = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            ProductGender? gender = null)
        {
            var (products, totalCount) = await _productRepository.GetPagedAsync(
                pageNumber, pageSize, brand, category, minPrice, maxPrice, gender);

            return (_mapper.Map<IEnumerable<ProductResponseDTO>>(products), totalCount);
        }

        public async Task<bool> UpdateStockAsync(int productId, int quantity)
        {
            return await _productRepository.UpdateStockAsync(productId, quantity);
        }

        public async Task<bool> IsInStockAsync(int productId)
        {
            return await _productRepository.IsInStockAsync(productId);
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetLowStockProductsAsync(int threshold = 5)
        {
            var products = await _productRepository.GetLowStockProductsAsync(threshold);
            return _mapper.Map<IEnumerable<ProductResponseDTO>>(products);
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetFeaturedProductsAsync()
        {
            var products = await _productRepository.GetFeaturedProductsAsync();
            return _mapper.Map<IEnumerable<ProductResponseDTO>>(products);
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetOnSaleProductsAsync()
        {
            var products = await _productRepository.GetOnSaleProductsAsync();
            return _mapper.Map<IEnumerable<ProductResponseDTO>>(products);
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetNewArrivalsAsync(int days = 30)
        {
            var products = await _productRepository.GetNewArrivalsAsync(days);
            return _mapper.Map<IEnumerable<ProductResponseDTO>>(products);
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetRelatedProductsAsync(int productId, int count = 4)
        {
            var products = await _productRepository.GetRelatedProductsAsync(productId, count);
            return _mapper.Map<IEnumerable<ProductResponseDTO>>(products);
        }
    }
}