using AutoMapper;
using chronovault_api.Models;
using chronovault_api.Repositories.Interfaces;
using chronovault_api.Services.Interfaces;
using chronovault_api.DTOs.Request;
using chronovault_api.DTOs.Response;

namespace chronovault_api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository,
            IUserCredentialRepository userCredentialRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
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

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var userCredentials = new UserCredential
            {
                UserId = user.Id,
                User = user,
                PasswordHash = passwordHash,
            };

            var createdUser = await _userRepository.CreateAsync(user, userCredentials);
            return createdUser != null ? _mapper.Map<UserResponseDTO>(createdUser) : null;
        }
    }
}