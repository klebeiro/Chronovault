using Microsoft.EntityFrameworkCore;
using chronovault_api.Models;
using chronovault_api.Repositories.Interfaces;
using chronovault_api.Infra.Data;

namespace chronovault_api.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ChronovaultDbContext _context;

        public ImageRepository(ChronovaultDbContext context)
        {
            _context = context;
        }

        public async Task<Image?> GetByIdAsync(int id)
        {
            return await _context.Images
                .Include(i => i.Product)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Image>> GetByProductIdAsync(int productId)
        {
            return await _context.Images
                .Where(i => i.ProductId == productId)
                .OrderBy(i => i.Order)
                .ToListAsync();
        }

        public async Task<Image> CreateAsync(Image image)
        {
            _context.Images.Add(image);
            await _context.SaveChangesAsync();
            return image;
        }

        public async Task<Image> UpdateAsync(Image image)
        {
            _context.Images.Update(image);
            await _context.SaveChangesAsync();
            return image;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image == null) return false;

            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByProductIdAsync(int productId)
        {
            var images = await _context.Images
                .Where(i => i.ProductId == productId)
                .ToListAsync();

            if (!images.Any()) return false;

            _context.Images.RemoveRange(images);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}