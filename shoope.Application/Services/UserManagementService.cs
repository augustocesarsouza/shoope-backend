using AutoMapper;
using Shoope.Application.DTOs;
using Shoope.Application.DTOs.Validations.Interfaces;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.CloudinaryConfigClass;
using Shoope.Infra.Data.UtilityExternal.Interface;

namespace Shoope.Application.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserCreateDTOValidator _userCreateDTOValidator;
        private readonly IUserCreateAccountFunction _userCreateAccountFunction;
        private readonly ICloudinaryUti _cloudinaryUti;

        public UserManagementService(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork, IUserCreateDTOValidator userCreateDTOValidator,
            IUserCreateAccountFunction userCreateAccountFunction, ICloudinaryUti cloudinaryUti)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userCreateDTOValidator = userCreateDTOValidator;
            _userCreateAccountFunction = userCreateAccountFunction;
            _cloudinaryUti = cloudinaryUti;
        }


        public async Task<ResultService<UserDTO>> CheckEmailAlreadyExists(string phone)
        {
            try
            {
                var user = await _userRepository.GetUserByPhone(phone);

                return ResultService.Ok(_mapper.Map<UserDTO>(user));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<UserDTO>(ex.Message);
            }
        }

        public async Task<ResultService<UserDTO>> Create(UserDTO? userDTO)
        {
            if (userDTO == null)
                return ResultService.Fail<UserDTO>("userDTO is null");

            if (userDTO.Password == null)
                return ResultService.Fail<UserDTO>("Password informed is null");

            var validationUser = _userCreateDTOValidator.ValidateDTO(userDTO);

            if (!validationUser.IsValid)
                return ResultService.RequestError<UserDTO>("validation error check the information", validationUser);

            string password = userDTO.Password;

            byte[] saltBytes = _userCreateAccountFunction.GenerateSalt();
            // Hash the password with the salt
            string hashedPassword = _userCreateAccountFunction.HashPassword(password, saltBytes);
            string base64Salt = Convert.ToBase64String(saltBytes);

            byte[] retrievedSaltBytes = Convert.FromBase64String(base64Salt);

            try
            {
                await _unitOfWork.BeginTransaction();

                Guid idUser = Guid.NewGuid();
                string randomName = GenerateRandomName(8);
                //User(Guid id, string? name, string? email, string? gender, string? phone, string? passwordHash, string? salt, string? cpf, DateTime? birthDate, string? token)

                User userCreate = new User();

                if (userDTO.Base64ImageUser != null)
                {
                    CloudinaryCreate result = await _cloudinaryUti.CreateMedia(userDTO.Base64ImageUser, "img-user", 320, 320);

                    if (result.ImgUrl == null || result.PublicId == null)
                    {
                        await _unitOfWork.Rollback();
                        return ResultService.Fail<UserDTO>("error when create ImgPerfil");
                    }

                    userCreate = new User(idUser, randomName, "", "", userDTO.Phone, hashedPassword, base64Salt, "", null, result.ImgUrl);
                }
                else
                {
                    userCreate = new User(idUser, randomName, "", "", userDTO.Phone, hashedPassword, base64Salt, "", null, null);
                }

                var data = await _userRepository.CreateAsync(userCreate);

                if (data == null)
                    return ResultService.Fail<UserDTO>("error when create user null value");

                var userReturnToFrontend = new UserDTO();
                userReturnToFrontend.SetName(data.Name);
                userReturnToFrontend.SetEmail(data.Email);

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<UserDTO>(userReturnToFrontend));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<UserDTO>(ex.Message);
            }
        }

        private static string GenerateRandomName(int length)
        {
            Random random = new Random();

            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            string prefix = "e_"; // Prefixo fixo
            string randomString = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return prefix + randomString;
        }

        public async Task<ResultService<UserDTO>> UpdateUserAll(UserUpdateAllDTO? userUpdateAllDTO)
        {
            try
            {
                if (userUpdateAllDTO == null)
                    return ResultService.Fail<UserDTO>("Error DTO it is null");

                if (userUpdateAllDTO.UserId == null)
                    return ResultService.Fail<UserDTO>("Error UserId it is null");

                var userToUpdate = await _userRepository.GetUserById(Guid.Parse(userUpdateAllDTO.UserId));

                if (userToUpdate == null)
                    return ResultService.Fail<UserDTO>("Error user Not Found");
                //User(string ? name, string ? email, string ? gender, string ? phone)

                if (userUpdateAllDTO.Base64StringImage != null && userToUpdate.UserImage == null)
                {
                    CloudinaryCreate result = await _cloudinaryUti.CreateMedia(userUpdateAllDTO.Base64StringImage, "img-user", 320, 320);

                    if (result.ImgUrl == null || result.PublicId == null)
                    {
                        await _unitOfWork.Rollback();
                        return ResultService.Fail<UserDTO>("error when create ImgPerfil");
                    }

                    userToUpdate.SetValueUpdateUser(userUpdateAllDTO.Name, userUpdateAllDTO.Email, userUpdateAllDTO.Gender, userUpdateAllDTO.Phone, result.ImgUrl);
                }
                else
                {
                    userToUpdate.SetValueUpdateUser(userUpdateAllDTO.Name, userUpdateAllDTO.Email, userUpdateAllDTO.Gender, userUpdateAllDTO.Phone, userToUpdate.UserImage);
                }


                var userUpdate = await _userRepository.UpdateAsync(userToUpdate);

                if (userUpdate == null)
                    return ResultService.Fail<UserDTO>("Error userUpdate it is null");

                return ResultService.Ok(new UserDTO(userUpdate.Id, userUpdate.Name, userUpdate.Email, userUpdate.Gender, userUpdate.Phone, null, null, null,
                    userUpdate.Cpf, userUpdate.BirthDate, null, userUpdate.UserImage));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<UserDTO>(ex.Message);
            }
        }

        public async Task<ResultService<UserDTO>> UpdateUser(UserUpdateFillDTO? userUpdateFillDTO)
        {
            if (userUpdateFillDTO == null)
                return ResultService.Fail<UserDTO>("Error DTO it is null");

            if (userUpdateFillDTO.Cpf == null)
                return ResultService.Fail<UserDTO>("Error Cpf null");

            if (userUpdateFillDTO.UserId == null)
                return ResultService.Fail<UserDTO>("Error UserId null");

            if (userUpdateFillDTO.BirthDate == null)
                return ResultService.Fail<UserDTO>("Error BirthDate null");

            try
            {
                if (userUpdateFillDTO.Cpf.Length > 11 || userUpdateFillDTO.Cpf.Length < 11)
                    return ResultService.Fail<UserDTO>("Is not a Cpf Valid");

                var userToUpdate = await _userRepository.GetUserById(Guid.Parse(userUpdateFillDTO.UserId));

                if (userToUpdate == null)
                    return ResultService.Fail<UserDTO>("Error user Not Found");

                var stringCortada = userUpdateFillDTO.BirthDate.Split('/');
                var dia = stringCortada[0];
                var mes = stringCortada[1];
                var ano = stringCortada[2];

                var birthDate = new DateTime(int.Parse(ano), int.Parse(mes), int.Parse(dia));
                var birthDateUtc = DateTime.SpecifyKind(birthDate, DateTimeKind.Utc); // TEM QUE ESTÁR NO FORMATO UTC PARA SALVAR NO BANCO POSTGRE

                userToUpdate.SetBirthdate(birthDateUtc);
                userToUpdate.SetCpf(userUpdateFillDTO.Cpf);

                // Suponha que `birthDateUtc` é a data recuperada do banco em UTC
                //DateTime birthDateUtc = userFromDb.BirthDate.Value;

                // Especificar o fuso horário do Brasil (ou o fuso horário do seu país)
                //TimeZoneInfo localZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

                // Converter o DateTime de UTC para o fuso horário local
                //DateTime birthDateLocal = TimeZoneInfo.ConvertTimeFromUtc(birthDateUtc, localZone);

                var userUpdate = await _userRepository.UpdateAsync(userToUpdate);

                if (userUpdate == null)
                    return ResultService.Fail<UserDTO>("Error userUpdate it is null");

                return ResultService.Ok(new UserDTO(null, userUpdate.Name, userUpdate.Email, null, null, null, null, null,
                    userUpdate.Cpf, userUpdate.BirthDate, null, userUpdate.UserImage));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<UserDTO>(ex.Message);
            }
        }
    }
}
