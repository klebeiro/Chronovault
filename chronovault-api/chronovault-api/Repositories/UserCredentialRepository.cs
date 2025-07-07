using Microsoft.EntityFrameworkCore;
using chronovault_api.Models;
using chronovault_api.Repositories.Interfaces;
using chronovault_api.Infra.Data;
using System.Security.Cryptography;

namespace chronovault_api.Repositories
{
    public class UserCredentialRepository : IUserCredentialRepository
    {
        private readonly ChronovaultDbContext _context;

        public UserCredentialRepository(ChronovaultDbContext context)
        {
            _context = context;
        }

        public async Task<UserCredential?> GetByUserIdAsync(int userId)
        {
            return await _context.UserCredentials
                .Include(uc => uc.User)
                .FirstOrDefaultAsync(uc => uc.UserId == userId);
        }

        public async Task<UserCredential> CreateAsync(UserCredential credential)
        {
            _context.UserCredentials.Add(credential);
            await _context.SaveChangesAsync();
            return credential;
        }

        public async Task<UserCredential> UpdateAsync(UserCredential credential)
        {
            _context.UserCredentials.Update(credential);
            await _context.SaveChangesAsync();
            return credential;
        }

        public async Task<bool> DeleteAsync(int userId)
        {
            var credential = await _context.UserCredentials.FindAsync(userId);
            if (credential == null) return false;

            _context.UserCredentials.Remove(credential);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ValidateCredentialsAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return false;

            var credential = await _context.UserCredentials.FirstOrDefaultAsync(uc => uc.UserId == user.Id);
            if (credential == null) return false;

            return VerifyPassword(password, credential.PasswordHash, credential.PasswordSalt);
        }

        private bool VerifyPassword(string password, byte[] hash, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                byte[] computedHash = pbkdf2.GetBytes(32);
                return computedHash.SequenceEqual(hash);
            }
        }
    }
}