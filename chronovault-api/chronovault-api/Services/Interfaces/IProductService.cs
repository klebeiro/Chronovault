using chronovault_api.DTOs.Request;
using chronovault_api.DTOs.Response;
using chronovault_api.Models.Enums;

namespace chronovault_api.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponseDTO?> GetByIdAsync(int id);
        Task<IEnumerable<ProductResponseDTO>> GetAllAsync();
        Task<ProductResponseDTO?> CreateAsync(ProductCreateDTO productCreateDTO);
    }
}