using AutoMapper;
using Moq;
using Shoope.Application.DTOs.Validations.Interfaces;
using Shoope.Domain.Repositories;

namespace Shoope.Application.ServicesTests
{
    public class CuponServiceConfiguration
    {
        public Mock<ICuponRepository> CuponRepositoryMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }
        public Mock<ICuponCreateDTOValidator> CuponCreateDTOValidatorMock { get; }

        public CuponServiceConfiguration()
        {
            CuponRepositoryMock = new();
            MapperMock = new();
            UnitOfWorkMock = new();
            CuponCreateDTOValidatorMock = new();
        }
    }
}
