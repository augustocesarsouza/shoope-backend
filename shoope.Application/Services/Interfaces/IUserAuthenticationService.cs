using Shoope.Application.DTOs;

namespace Shoope.Application.Services.Interfaces
{
    public interface IUserAuthenticationService
    {
        public Task<ResultService<UserDTO>> GetByIdInfoUser(string userId);
        public Task<ResultService<UserLoginDTO>> Login(string phone, string password);
        public Task<ResultService<UserLoginDTO>> VerifyPasswordUser(string phone, string password);
        public Task<ResultService<UserPasswordUpdateDTO>> ChangePasswordUser(UserChangePasswordDTO userChangePasswordDTO);
        public Task<ResultService<CodeSendEmailUserDTO>> SendCodeEmail(UserDTO? userDTO);
        public Task<ResultService<UserDTO>> Verfic(UserConfirmCodeEmailDTO userConfirmCodeEmailDTO);
    }
}
