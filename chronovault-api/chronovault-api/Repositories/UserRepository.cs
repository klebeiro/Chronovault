using Microsoft.EntityFrameworkCore;
using chronovault_api.Models;
using chronovault_api.Repositories.Interfaces;
using chronovault_api.Infra.Data;

namespace chronovault_api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChronovaultDbContext _context;

        public UserRepository(ChronovaultDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Address)
                .ToListAsync();
        }

        public async Task<User> CreateAsync(User user, UserCredential userCredentials)
        {
            user.CreatedAt = DateTime.Now;
            _context.Users.Add(user);
            _context.UserCredentials.Add(userCredentials);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}