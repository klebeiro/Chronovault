using chronovault_api.DTOs.Request;
using chronovault_api.DTOs.Response;

namespace chronovault_api.Services.Interfaces
{    public interface IUserService
    {
        Task<UserResponseDTO> GetByIdAsync(int id);
        Task<IEnumerable<UserResponseDTO>> GetAllAsync();
        Task<UserResponseDTO> CreateAsync(UserCreateDTO dto);
    }
}