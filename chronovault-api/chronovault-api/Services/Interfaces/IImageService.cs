using chronovault_api.DTOs.Request;
using chronovault_api.DTOs.Response;

namespace chronovault_api.Services.Interfaces
{
    public interface IImageService
    {
        Task<ImageResponseDTO?> GetByIdAsync(int id);
        Task<IEnumerable<ImageResponseDTO>> GetByProductIdAsync(int productId);
        Task<ImageResponseDTO?> CreateAsync(ImageCreateDTO imageCreateDTO);
        Task<ImageResponseDTO?> UpdateAsync(int id, ImageUpdateDTO imageUpdateDTO);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteByProductIdAsync(int productId);
    }
}