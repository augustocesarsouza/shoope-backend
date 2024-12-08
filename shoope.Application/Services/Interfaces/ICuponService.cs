using Shoope.Application.DTOs;

namespace Shoope.Application.Services.Interfaces
{
    public interface ICuponService
    {
        public Task<ResultService<CuponDTO>> GetCuponById(Guid cuponId);
        public Task<ResultService<CuponDTO>> CreateAsync(CuponDTO? cuponDTO);
    }
}
