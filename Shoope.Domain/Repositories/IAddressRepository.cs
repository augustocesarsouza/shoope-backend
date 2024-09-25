using Shoope.Domain.Entities;

namespace Shoope.Domain.Repositories
{
    public interface IAddressRepository
    {
        public Task<Address?> GetAddressById(Guid addressId);
        public Task<Address?> GetAddressByUserId(Guid userId);
        public Task<Address?> CreateAsync(Address address);
        public Task<Address?> UpdateUser(Address address);
        public Task<Address?> Delete(Address address);
    }
}
