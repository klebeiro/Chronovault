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
    }
}