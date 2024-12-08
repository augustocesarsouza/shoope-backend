using Shoope.Application.DTOs;

namespace Shoope.Application.Services.Interfaces
{
    public interface IUserSellerProductService
    {
        public Task<ResultService<UserSellerProductDTO>> GetById(Guid userSellerProductId);
        public Task<ResultService<UserSellerProductDTO>> Create(UserSellerProductDTO userSellerProductDTO);
    }
}
