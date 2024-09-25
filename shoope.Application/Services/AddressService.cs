using AutoMapper;
using Microsoft.Extensions.Configuration;
using Shoope.Application.DTOs;
using Shoope.Application.DTOs.Validations.Interfaces;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.UtilityExternal.Interface;

namespace Shoope.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAddressCreateDTOValidator _addressCreateDTOValidator;
        private readonly IConfiguration _configuration;
        // criar o "Validator" de criar uma conta

        public AddressService(IAddressRepository addressRepository, IMapper mapper, IUnitOfWork unitOfWork,
            IAddressCreateDTOValidator addressCreateDTOValidator, IConfiguration configuration)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _addressCreateDTOValidator = addressCreateDTOValidator;
            _configuration = configuration;
        }

        public async Task<ResultService<AddressDTO>> GetAddressById(Guid addressId)
        {
            var address = await _addressRepository.GetAddressById(addressId);

            return ResultService.Ok(_mapper.Map<AddressDTO>(address));
        }

        public async Task<ResultService<AddressDTO>> GetAddressByUserId(Guid userId)
        {
            var address = await _addressRepository.GetAddressByUserId(userId);

            return ResultService.Ok(_mapper.Map<AddressDTO>(address));
        }

        // TESTAR A CRIAÇÃO E TALS E DEPOIS PEGANDO BY "ID DO USUARIO" AS INFORMAÇÕES DE "ADDRESS" DELE

        public async Task<ResultService<AddressDTO>> CreateAsync(AddressDTO addressDTO)
        {
            if (addressDTO == null)
                return ResultService.Fail<AddressDTO>("error dto null");

            var validationAddress = _addressCreateDTOValidator.ValidateDTO(addressDTO);

            if (!validationAddress.IsValid)
                return ResultService.RequestError<AddressDTO>("validation error check the information", validationAddress);

            try
            {
                await _unitOfWork.BeginTransaction();

                Guid addressId = Guid.NewGuid();
                var address = new Address(addressId, addressDTO.FullName ?? "", addressDTO.PhoneNumber ?? "", addressDTO.Cep ?? "", addressDTO.StateCity ?? "", 
                    addressDTO.Neighborhood ?? "", addressDTO.Street ?? "", addressDTO.NumberHome ?? "", addressDTO.Complement ?? "", addressDTO.UserId);

                var createAddres = await _addressRepository.CreateAsync(address);

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<AddressDTO>(createAddres));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<AddressDTO>(ex.Message);
            }
        }

        public Task<ResultService<AddressDTO>> Delete(Guid addressId)
        {
            throw new NotImplementedException();
        }

        public Task<ResultService<AddressDTO>> UpdateUser(AddressDTO addressDTO)
        {
            throw new NotImplementedException();
        }
    }
}
