using Shoope.Domain.Entities;

namespace Shoope.Domain.Repositories
{
    public interface IUserCuponRepository : IGenericRepository<UserCupon>
    {
        public Task<List<UserCupon>> GetAllCuponByUserId(Guid userId);
    }
}
