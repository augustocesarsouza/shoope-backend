using Shoope.Domain.Entities;

namespace Shoope.Domain.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User?> GetUserById(Guid id);
        public Task<User?> GetUserByPhoneInfoUpdate(string phone);
        public Task<User?> GetUserByIdInfoUser(Guid id);
        public Task<User?> GetUserByPhone(string phone);
        public Task<User?> GetUserByName(string name);
        public Task<User?> GetIfUserExistEmail(string email);
        public Task<User?> GetUserInfoToLogin(string phone);
    }
}
