using AutoMapper;
using chronovault_api.DTOs.Request;
using chronovault_api.DTOs.Response;
using chronovault_api.Repositories.Interfaces;
using chronovault_api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace chronovault_api.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly IUserRepository _userRepository;
        private readonly IUserCredentialRepository _userCredentialRepository;
        private readonly IMapper _mapper;

        public JwtService(IConfiguration configuration, IUserRepository userRepository, IUserCredentialRepository userCredentialRepository, IMapper mapper)
        {
            _configuration = configuration;
            _secretKey = _configuration["Jwt:SecretKey"];
            _issuer = _configuration["Jwt:Issuer"];
            _audience = _configuration["Jwt:Audience"];
            _userRepository = userRepository;
            _userCredentialRepository = userCredentialRepository;
            _mapper = mapper;
        }

        public async Task<AuthResponseDTO> GenerateToken(UserCredentialDTO userCredentials)
        {
            var user = await _userRepository.GetByEmailAsync(userCredentials.Email);
            if (user == null) return new AuthResponseDTO();

            var registeredCredential = await _userCredentialRepository.GetByUserIdAsync(user.Id);
            if (registeredCredential == null) return new AuthResponseDTO();

            var validPassword = BCrypt.Net.BCrypt.Verify(userCredentials.Password, registeredCredential.PasswordHash);
            if (!validPassword) return new AuthResponseDTO(); 

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, registeredCredential.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,
                    new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64)
            };

            var tokenConfig = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: credentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenConfig);

            return new AuthResponseDTO()
            {
                Token = token,
                User = _mapper.Map<UserResponseDTO>(user)
            };
        }
    }
}
