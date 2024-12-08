using Microsoft.EntityFrameworkCore;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.Context;

namespace Shoope.Infra.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetUserById(Guid id)
        {
            var user = await _context
                .Users
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetUserByPhoneInfoUpdate(string phone)
        {
            var user = await _context
                .Users
                .Where(u => u.Phone == phone)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetUserByIdInfoUser(Guid id)
        {
            var user = await _context
                .Users
                .Where(u => u.Id == id)
                .Select(x => new User(x.Id, x.Name, x.Email, x.Gender, x.Phone, null, null, x.Cpf, x.BirthDate, x.UserImage))
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetUserByPhone(string phone)
        {
            var user = await _context
                .Users
                .Where(u => u.Phone == phone)
                .Select(x => new User(x.Id, x.Name, null, null, null, null, null, null, null, x.UserImage))
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetUserByName(string name)
        {
            var user = await _context
                .Users
                .Where(u => u.Name == name)
                .Select(x => new User(x.Id, x.Name, null, null, null, null, null, null, null, x.UserImage))
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetIfUserExistEmail(string email)
        {
            var user = await _context
                .Users
                .Where(u => u.Email == email)
                .Select(x => new User(x.Id, x.Name, null, null, null, null, null, null, null, x.UserImage))
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetUserInfoToLogin(string phone)
        {
            var user = await _context
                .Users
                .Where(u => u.Phone == phone)
                .Select(x => new User(x.Id, x.Name, x.Email, null, x.Phone, x.PasswordHash, x.Salt, null, null, x.UserImage))
                .FirstOrDefaultAsync();

            return user;
        }
    }
}
