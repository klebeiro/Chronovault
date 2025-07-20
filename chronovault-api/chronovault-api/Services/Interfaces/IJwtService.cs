using chronovault_api.DTOs.Request;
using chronovault_api.DTOs.Response;
using chronovault_api.Models;
using System.Security.Claims;

namespace chronovault_api.Services.Interfaces
{
    public interface IJwtService
    {
        Task<AuthResponseDTO> GenerateToken(UserCredentialDTO userCredentials);
    }
}
