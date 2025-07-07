using AutoMapper;
using chronovault_api.Models;
using chronovault_api.Repositories.Interfaces;
using chronovault_api.Services.Interfaces;
using chronovault_api.DTOs.Request;
using chronovault_api.DTOs.Response;
using System.Runtime.CompilerServices;

namespace chronovault_api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserCredentialRepository _userCredentialRepository;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository,
            IUserCredentialRepository userCredentialRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _userCredentialRepository = userCredentialRepository;
            _mapper = mapper;
        }

        public async Task<UserResponseDTO?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user != null ? _mapper.Map<UserResponseDTO>(user) : null;
        }

        public async Task<IEnumerable<UserResponseDTO>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserResponseDTO>>(users);
        }

        public async Task<UserResponseDTO?> CreateAsync(UserCreateDTO dto)
        {
            var user = _mapper.Map<User>(dto);
            user.CreatedAt = DateTime.UtcNow;

            var createdUser = await _userRepository.CreateAsync(user);
            return createdUser != null ? _mapper.Map<UserResponseDTO>(createdUser) : null;
        }

        public async Task<UserResponseDTO?> UpdateAsync(int id, UserUpdateDTO dto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            _mapper.Map(dto, user);
            user.UpdatedAt = DateTime.UtcNow;

            var updatedUser = await _userRepository.UpdateAsync(user);
            return updatedUser != null ? _mapper.Map<UserResponseDTO>(updatedUser) : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        public async Task<bool> ValidateCredentialsAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null) return false;

            var credential = await _userCredentialRepository.GetByUserIdAsync(user.Id);
            if (credential == null) return false;

            return BCrypt.Net.BCrypt.Verify(password, Convert.ToBase64String(credential.PasswordHash));
        }

        public async Task<bool> ChangePasswordAsync(int userId, string newPassword)
        {
            var credential = await _userCredentialRepository.GetByUserIdAsync(userId);
            if (credential == null) return false;

            // Hash da nova senha
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword, salt);

            credential.PasswordHash = Convert.FromBase64String(hashedPassword);
            credential.PasswordSalt = Convert.FromBase64String(salt);

            var updated = await _userCredentialRepository.UpdateAsync(credential);
            return updated != null;
        }
    }
}