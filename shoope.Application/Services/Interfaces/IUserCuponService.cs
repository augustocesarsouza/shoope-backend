using Shoope.Application.DTOs;

namespace Shoope.Application.Services.Interfaces
{
    public interface IUserCuponService
    {
        public Task<ResultService<List<UserCuponDTO>>> GetAllCuponByUserId(Guid userId);
        public Task<ResultService<UserCuponDTO>> Create(UserCuponDTO? userCuponDTO);
    }
}
