using chronovault_api.Models;

namespace chronovault_api.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<Image?> GetByIdAsync(int id);
        Task<IEnumerable<Image>> GetByProductIdAsync(int productId);
        Task<Image> CreateAsync(Image image);
        Task<Image> UpdateAsync(Image image);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteByProductIdAsync(int productId);
    }
}