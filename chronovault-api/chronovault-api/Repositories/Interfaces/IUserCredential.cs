using chronovault_api.Models;

namespace chronovault_api.Repositories.Interfaces
{
    public interface IUserCredentialRepository
    {
        Task<UserCredential?> GetByUserIdAsync(int userId);
        Task<UserCredential> CreateAsync(UserCredential credential);
        Task<UserCredential> UpdateAsync(UserCredential credential);
        Task<bool> DeleteAsync(int userId);
        Task<bool> ValidateCredentialsAsync(string email, string password);
    }
}