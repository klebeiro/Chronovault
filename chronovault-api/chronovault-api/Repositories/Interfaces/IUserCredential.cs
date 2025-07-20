using chronovault_api.Models;

namespace chronovault_api.Repositories.Interfaces
{
    public interface IUserCredentialRepository
    {
        Task<UserCredential?> GetByUserIdAsync(int userId);
        Task<UserCredential> CreateAsync(UserCredential credential);
    }
}