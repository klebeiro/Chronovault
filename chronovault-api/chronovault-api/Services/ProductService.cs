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

        public async Task<ProductResponseDTO?> CreateAsync(ProductCreateDTO productCreateDTO)
        {
            var product = _mapper.Map<Product>(productCreateDTO);
            var created = await _productRepository.CreateAsync(product);
            return created != null ? _mapper.Map<ProductResponseDTO>(created) : null;
        }
    }
}