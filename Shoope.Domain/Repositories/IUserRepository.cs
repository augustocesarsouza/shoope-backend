using Shoope.Domain.Entities;

namespace Shoope.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<User?> GetUserById(Guid id);
        public Task<User?> GetUserByPhoneInfoUpdate(string phone);
        public Task<User?> GetUserByIdInfoUser(Guid id);
        public Task<User?> GetUserByPhone(string phone);
        public Task<User?> GetUserByName(string name);
        public Task<User?> GetIfUserExistEmail(string email);
        public Task<User?> GetUserInfoToLogin(string phone);
        public Task<User?> CreateAsync(User user);
        public Task<User?> UpdateUser(User user);
        public Task<User?> Delete(User user);
    }
}
