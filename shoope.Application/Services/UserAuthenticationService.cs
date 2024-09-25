using AutoMapper;
using Shoope.Application.CodeRandomUser;
using Shoope.Application.DTOs;
using Shoope.Application.DTOs.Validations.Interfaces;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.SendEmailUser.Interface;
using System.Text;

namespace Shoope.Application.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserCreateAccountFunction _userCreateAccountFunction;
        private readonly ISendEmailUser _sendEmailUser;
        private readonly static CodeRandomDictionary _codeRandomDictionary = new();
        private readonly IUserSendCodeEmailDTOValidator _userSendCodeEmailDTOValidator;

        public UserAuthenticationService(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork, IUserCreateAccountFunction userCreateAccountFunction,
            ISendEmailUser sendEmailUser, IUserSendCodeEmailDTOValidator userSendCodeEmailDTOValidator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userCreateAccountFunction = userCreateAccountFunction;
            _sendEmailUser = sendEmailUser;
            _userSendCodeEmailDTOValidator = userSendCodeEmailDTOValidator;
        }

        public async Task<ResultService<UserDTO>> GetByIdInfoUser(string userId)
        {
            try
            {
                var infoUser = await _userRepository.GetUserByIdInfoUser(Guid.Parse(userId));

                if (infoUser == null)
                    return ResultService.Fail<UserDTO>("Error user null");

                return ResultService.Ok(_mapper.Map<UserDTO>(infoUser));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<UserDTO>(ex.Message);
            }
        }

        public async Task<ResultService<UserLoginDTO>> Login(string phone, string password)
        {
            try
            {
                // In a real scenario, you would retrieve these values from your database
                //var user = _dbContext.Usertests.Where(x => x.Mobile == verify.MobileNo).Select(x => x).FirstOrDefault();
                var user = await _userRepository.GetUserInfoToLogin(phone);

                if (user == null)
                    return ResultService.Fail<UserLoginDTO>("Error user null");

                string storedHashedPassword = user.GetPasswordHash();// "hashed_password_from_database";
                                                                     //string storedSalt = user.Salt; //"salt_from_database";
                byte[] storedSaltBytes = Convert.FromBase64String(user.GetSalt());
                string enteredPassword = password; //"user_entered_password";

                // Convert the stored salt and entered password to byte arrays
                // byte[] storedSaltBytes = Convert.FromBase64String(user.Salt);
                byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(enteredPassword);

                // Concatenate entered password and stored salt
                byte[] saltedPassword = new byte[enteredPasswordBytes.Length + storedSaltBytes.Length];
                Buffer.BlockCopy(enteredPasswordBytes, 0, saltedPassword, 0, enteredPasswordBytes.Length);
                Buffer.BlockCopy(storedSaltBytes, 0, saltedPassword, enteredPasswordBytes.Length, storedSaltBytes.Length);

                var userReturnToFrontend = new UserDTO();
                userReturnToFrontend.SetName(user.Name);
                userReturnToFrontend.SetEmail(user.Email);
                userReturnToFrontend.SetId(user.GetId());

                // Hash the concatenated value
                string enteredPasswordHash = _userCreateAccountFunction.HashPassword(enteredPassword, storedSaltBytes);

                // Compare the entered password hash with the stored hash
                if (enteredPasswordHash == storedHashedPassword)
                {
                    return ResultService.Ok(new UserLoginDTO(true, userReturnToFrontend));
                }
                else
                {
                    return ResultService.Fail(new UserLoginDTO(false, userReturnToFrontend));
                }
            }
            catch (Exception ex)
            {
                return ResultService.Fail<UserLoginDTO>(ex.Message);
            }
        }

        public async Task<ResultService<CodeSendEmailUserDTO>> SendCodeEmail(UserDTO userDTO)
        {
            try
            {
                if(userDTO == null)
                    return ResultService.Fail<CodeSendEmailUserDTO>("error deto user null");

                var resultValidationUserDTO = _userSendCodeEmailDTOValidator.ValidateDTO(userDTO);

                if (!resultValidationUserDTO.IsValid)
                    return ResultService.RequestError<CodeSendEmailUserDTO>("validation error check the information", resultValidationUserDTO);

                var user = await _userRepository.GetUserByName(userDTO.Name ?? "");

                if (user == null)
                    return ResultService.Fail<CodeSendEmailUserDTO>("error user user null");

                if(userDTO.Email == null)
                    return ResultService.Fail<CodeSendEmailUserDTO>("User Not Provided Email");

                var checkIfUserExist = await _userRepository.GetIfUserExistEmail(userDTO.Email);

                if (checkIfUserExist != null)
                    return ResultService.Ok(new CodeSendEmailUserDTO(null, false, true));

                user.SetEmail(userDTO.Email ?? "");

                if (user == null)
                    return ResultService.Fail<CodeSendEmailUserDTO>("error user is null");

                var randomCode = GerarNumeroAleatorio();
                _codeRandomDictionary.Add(user.Id.ToString(), randomCode);

                //_sendEmailUser.SendCodeRandom(user, randomCode);
                var codeSend = new CodeSendEmailUserDTO(randomCode.ToString(), true, false);

                return ResultService.Ok(codeSend);
            }
            catch (Exception ex)
            {
                return ResultService.Fail<CodeSendEmailUserDTO>(ex.Message);
            }
        }

        public async Task<ResultService<UserDTO>> Verfic(UserConfirmCodeEmailDTO userConfirmCodeEmailDTO)
        {
            if(userConfirmCodeEmailDTO == null)
                return ResultService.Fail<UserDTO>("DTO NULL");

            if(userConfirmCodeEmailDTO.UserId == null || userConfirmCodeEmailDTO.Code == null || userConfirmCodeEmailDTO.Email == null)
                return ResultService.Fail<UserDTO>("DTO property Is Null");

            if (_codeRandomDictionary.Container(userConfirmCodeEmailDTO.UserId, int.Parse(userConfirmCodeEmailDTO.Code)))
            {
                await _unitOfWork.BeginTransaction();

                //var checkIfUserExist = await _userRepository.GetIfUserExistEmail(userConfirmCodeEmailDTO.Email);

                //if(checkIfUserExist != null)
                //    return ResultService.Fail<UserDTO>("User Not Found");

                var userUpdate = await _userRepository.GetUserById(Guid.Parse(userConfirmCodeEmailDTO.UserId));

                if(userUpdate == null)
                    return ResultService.Fail<UserDTO>("User Not Found");

                userUpdate.SetEmail(userConfirmCodeEmailDTO.Email);

                var userUpdateNow = await _userRepository.UpdateUser(userUpdate);

                _codeRandomDictionary.Remove(userConfirmCodeEmailDTO.UserId);

                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<UserDTO>(userUpdateNow));
            }
            else
            {
                return ResultService.Fail<UserDTO>("error");
            }
        }

        private static int GerarNumeroAleatorio()
        {
            Random random = new Random();
            return random.Next(100000, 1000000);
        }
    }
}
