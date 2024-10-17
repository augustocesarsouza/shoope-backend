using Shoope.Application.DTOs;

namespace Shoope.Application.Services.Interfaces
{
    public interface IUserManagementService
    {
        public Task<ResultService<UserDTO>> CheckEmailAlreadyExists(string phone);
        public Task<ResultService<UserDTO>> Create(UserDTO? userDTO);
        public Task<ResultService<UserDTO>> UpdateUserAll(UserUpdateAllDTO? userUpdateAllDTO);
        public Task<ResultService<UserDTO>> UpdateUser(UserUpdateFillDTO? userUpdateFillDTO);
    }
}
