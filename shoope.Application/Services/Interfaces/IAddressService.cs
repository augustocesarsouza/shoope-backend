using Shoope.Application.DTOs;

namespace Shoope.Application.Services.Interfaces
{
    public interface IAddressService
    {
        public Task<ResultService<AddressDTO>> GetAddressById(Guid addressId);
        public Task<ResultService<List<AddressDTO>>> GetAddressByUserId(Guid userId);
        public Task<ResultService<AddressDTO>> CreateAsync(AddressDTO? addressDTO);
        public Task<ResultService<AddressDTO>> UpdateAddressUser(AddressDTO? addressDTO);
        public Task<ResultService<AddressDTO>> UpdateAsyncOnlyDefaultAddress(AddressDTO? addressDTO);
        public Task<ResultService<AddressDTO>> Delete(Guid addressId);
    }
}
