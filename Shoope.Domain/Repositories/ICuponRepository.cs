using Shoope.Domain.Entities;

namespace Shoope.Domain.Repositories
{
    public interface ICuponRepository : IGenericRepository<Cupon>
    {
        public Task<Cupon?> GetCuponById(Guid cuponId);
    }
}
