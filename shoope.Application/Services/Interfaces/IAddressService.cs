using Shoope.Application.DTOs;

namespace Shoope.Application.Services.Interfaces
{
    public interface IAddressService
    {
        public Task<ResultService<AddressDTO>> GetAddressById(Guid addressId);
        public Task<ResultService<AddressDTO>> GetAddressByUserId(Guid userId);
        public Task<ResultService<AddressDTO>> CreateAsync(AddressDTO addressDTO);
        public Task<ResultService<AddressDTO>> UpdateUser(AddressDTO addressDTO);
        public Task<ResultService<AddressDTO>> Delete(Guid addressId);
    }
}
