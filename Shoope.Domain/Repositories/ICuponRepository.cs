using Shoope.Domain.Entities;

namespace Shoope.Domain.Repositories
{
    public interface ICuponRepository
    {
        public Task<Cupon?> GetCuponById(Guid cuponId);
        public Task<Cupon?> CreateAsync(Cupon cupon);
    }
}
