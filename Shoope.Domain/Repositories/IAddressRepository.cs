using Shoope.Domain.Entities;

namespace Shoope.Domain.Repositories
{
    public interface IAddressRepository
    {
        public Task<Address?> GetAddressById(Guid addressId);
        public Task<List<Address>?> GetAddressByUserId(Guid userId);
        public Task<Address?> VerifyIfUserAlreadyHaveAddress(Guid userId);
        public Task<Address?> GetAddressDefault();
        public Task<Address?> CreateAsync(Address address);
        public Task<Address?> UpdateUser(Address address);
        public Task<Address?> Delete(Address address);
    }
}
