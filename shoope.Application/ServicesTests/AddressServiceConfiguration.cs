using AutoMapper;
using Moq;
using Shoope.Application.DTOs.Validations.Interfaces;
using Shoope.Domain.Repositories;

namespace Shoope.Application.ServicesTests
{
    public class AddressServiceConfiguration
    {
        public Mock<IAddressRepository> AddressRepositoryMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }
        public Mock<IAddressCreateDTOValidator> AddressCreateDTOValidatorMock { get; }
        public Mock<IAddressUpdateDTOValidator> AddressUpdateDTOValidatorMock { get; }
        public Mock<IAddressUpdateOnlyDefaultDTOValidator> AddressUpdateOnlyDefaultDTOValidatorMock { get; }

        public AddressServiceConfiguration()
        {
            AddressRepositoryMock = new();
            MapperMock = new();
            UnitOfWorkMock = new();
            AddressCreateDTOValidatorMock = new();
            AddressUpdateDTOValidatorMock = new();
            AddressUpdateOnlyDefaultDTOValidatorMock = new();
        }
    }
}