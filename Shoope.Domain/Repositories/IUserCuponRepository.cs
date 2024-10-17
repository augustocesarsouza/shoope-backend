using Shoope.Domain.Entities;

namespace Shoope.Domain.Repositories
{
    public interface IUserCuponRepository
    {
        public Task<List<UserCupon>> GetAllCuponByUserId(Guid userId);
        public Task<UserCupon> Create(UserCupon userCupon);
    }
}
