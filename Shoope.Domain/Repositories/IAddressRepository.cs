using Shoope.Domain.Entities;

namespace Shoope.Domain.Repositories
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        public Task<Address?> GetAddressById(Guid addressId);
        public Task<List<Address>?> GetAddressByUserId(Guid userId);
        public Task<Address?> VerifyIfUserAlreadyHaveAddress(Guid userId);
        public Task<Address?> GetAddressDefault();
    }
}
