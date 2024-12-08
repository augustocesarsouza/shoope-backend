using AutoMapper;
using FluentValidation.Results;
using Shoope.Application.DTOs;
using Shoope.Application.DTOs.Validations.Interfaces;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;

namespace Shoope.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAddressCreateDTOValidator _addressCreateDTOValidator;
        private readonly IAddressUpdateDTOValidator _addressUpdateDTOValidator;
        private readonly IAddressUpdateOnlyDefaultDTOValidator _addressUpdateOnlyDefaultDTOValidator;

        public AddressService(IAddressRepository addressRepository, IMapper mapper, IUnitOfWork unitOfWork,
            IAddressCreateDTOValidator addressCreateDTOValidator, IAddressUpdateDTOValidator addressUpdateDTOValidator,
            IAddressUpdateOnlyDefaultDTOValidator addressUpdateOnlyDefaultDTOValidator)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _addressCreateDTOValidator = addressCreateDTOValidator;
            _addressUpdateDTOValidator = addressUpdateDTOValidator;
            _addressUpdateOnlyDefaultDTOValidator = addressUpdateOnlyDefaultDTOValidator;
        }

        public async Task<ResultService<AddressDTO>> GetAddressById(Guid addressId)
        {
            try
            {
                var address = await _addressRepository.GetAddressById(addressId);

                return ResultService.Ok(_mapper.Map<AddressDTO>(address));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<AddressDTO>(ex.Message);
            }
        }

        public async Task<ResultService<List<AddressDTO>>> GetAddressByUserId(Guid userId)
        {
            try
            {
                var address = await _addressRepository.GetAddressByUserId(userId);

                return ResultService.Ok(_mapper.Map<List<AddressDTO>>(address));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<List<AddressDTO>>(ex.Message);
            }
        }

        // TESTAR A CRIAÇÃO E TALS E DEPOIS PEGANDO BY "ID DO USUARIO" AS INFORMAÇÕES DE "ADDRESS" DELE

        public async Task<ResultService<AddressDTO>> CreateAsync(AddressDTO? addressDTO)
        {
            if (addressDTO == null)
                return ResultService.Fail<AddressDTO>("error dto null");
            
            ValidationResult validationAddress = _addressCreateDTOValidator.ValidateDTO(addressDTO);

            if (!validationAddress.IsValid)
                return ResultService.RequestError<AddressDTO>("validation error check the information", validationAddress);

            try
            {
                await _unitOfWork.BeginTransaction();

                var verifyIfExistAddressRegistered = await _addressRepository.VerifyIfUserAlreadyHaveAddress(addressDTO.UserId);

                Guid addressId = Guid.NewGuid();
                var address = new Address();

                if (verifyIfExistAddressRegistered == null)
                {
                    address.SetValueToCreateAddress(addressId, addressDTO.FullName, addressDTO.PhoneNumber, addressDTO.Cep, addressDTO.StateCity,
                    addressDTO.Neighborhood, addressDTO.Street, addressDTO.NumberHome, addressDTO.Complement ?? "", 1, addressDTO.UserId);
                }else
                {
                    address.SetValueToCreateAddress(addressId, addressDTO.FullName, addressDTO.PhoneNumber, addressDTO.Cep, addressDTO.StateCity,
                    addressDTO.Neighborhood, addressDTO.Street, addressDTO.NumberHome, addressDTO.Complement ?? "", 0, addressDTO.UserId);
                }

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

        public async Task<ResultService<AddressDTO>> UpdateAddressUser(AddressDTO? addressDTO)
        {
            if (addressDTO == null)
                return ResultService.Fail<AddressDTO>("error dto null");

            var validationAddress = _addressUpdateDTOValidator.ValidateDTO(addressDTO);

            if (!validationAddress.IsValid)
                return ResultService.RequestError<AddressDTO>("validation error check the information", validationAddress);

            try
            {
                await _unitOfWork.BeginTransaction();

                var addressDb = await _addressRepository.GetAddressById(addressDTO.Id);

                if(addressDb == null)
                    return ResultService.Fail<AddressDTO>("error not found address");

                addressDb.SetValueToUpdateAddress(addressDTO.FullName, addressDTO.PhoneNumber, addressDTO.Cep, addressDTO.StateCity, addressDTO.Neighborhood, addressDTO.Street,
                    addressDTO.NumberHome, addressDTO.Complement);

                var createAddres = await _addressRepository.UpdateAsync(addressDb);

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<AddressDTO>(createAddres));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<AddressDTO>(ex.Message);
            }
        }

        public async Task<ResultService<AddressDTO>> UpdateAsyncOnlyDefaultAddress(AddressDTO? addressDTO)
        {
            if (addressDTO == null)
                return ResultService.Fail<AddressDTO>("Error DTO Null");

            var validationAddress = _addressUpdateOnlyDefaultDTOValidator.ValidateDTO(addressDTO);

            if (!validationAddress.IsValid)
                return ResultService.RequestError<AddressDTO>("validation error check the information", validationAddress);

            try
            {
                await _unitOfWork.BeginTransaction();

                var addressDb = await _addressRepository.GetAddressById(addressDTO.Id);

                if (addressDb == null)
                    return ResultService.Fail<AddressDTO>("error not found address");

                var addressDefault = await _addressRepository.GetAddressDefault();

                if (addressDefault == null)
                    return ResultService.Fail<AddressDTO>("error it was not possible found address default");

                addressDefault.SetDefaultAddress(0);

                var updateAddressDefault = await _addressRepository.UpdateAsync(addressDefault);

                addressDb.SetDefaultAddress(addressDTO.DefaultAddress);

                var updateAddress = await _addressRepository.UpdateAsync(addressDb);

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<AddressDTO>(updateAddress));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<AddressDTO>(ex.Message);
            }
        }

        public async Task<ResultService<AddressDTO>> Delete(Guid addressId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var addressDbDelete = await _addressRepository.GetAddressById(addressId);

                if (addressDbDelete == null)
                    return ResultService.Fail<AddressDTO>("error not found address");

                var createAddres = await _addressRepository.DeleteAsync(addressDbDelete);

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<AddressDTO>(createAddres));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<AddressDTO>(ex.Message);
            }
        }
    }
}
