using AutoMapper;
using chronovault_api.Models;
using chronovault_api.Repositories.Interfaces;
using chronovault_api.Services.Interfaces;
using chronovault_api.DTOs.Request;
using chronovault_api.DTOs.Response;

namespace chronovault_api.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public ImageService(IImageRepository imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<ImageResponseDTO?> GetByIdAsync(int id)
        {
            var image = await _imageRepository.GetByIdAsync(id);
            return image != null ? _mapper.Map<ImageResponseDTO>(image) : null;
        }

        public async Task<IEnumerable<ImageResponseDTO>> GetByProductIdAsync(int productId)
        {
            var images = await _imageRepository.GetByProductIdAsync(productId);
            return _mapper.Map<IEnumerable<ImageResponseDTO>>(images);
        }

        public async Task<ImageResponseDTO?> CreateAsync(ImageCreateDTO dto)
        {
            var image = _mapper.Map<Image>(dto);
            image.CreatedAt = DateTime.UtcNow;

            var createdImage = await _imageRepository.CreateAsync(image);
            return createdImage != null ? _mapper.Map<ImageResponseDTO>(createdImage) : null;
        }

        public async Task<ImageResponseDTO?> UpdateAsync(int id, ImageUpdateDTO dto)
        {
            var image = await _imageRepository.GetByIdAsync(id);
            if (image == null) return null;

            _mapper.Map(dto, image);
            image.UpdatedAt = DateTime.UtcNow;

            var updatedImage = await _imageRepository.UpdateAsync(image);
            return updatedImage != null ? _mapper.Map<ImageResponseDTO>(updatedImage) : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _imageRepository.DeleteAsync(id);
        }

        public async Task<bool> DeleteByProductIdAsync(int productId)
        {
            return await _imageRepository.DeleteByProductIdAsync(productId);
        }
    }
}