using AutoMapper;
using Moq;
using Shoope.Application.DTOs.Validations.Interfaces;
using Shoope.Domain.Repositories;

namespace Shoope.Application.ServicesTests
{
    public class PromotionUserServiceConfiguration
    {
        public Mock<IPromotionUserRepository> PromotionUserRepositoryMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }
        public Mock<IPromotionUserCreateDTOValidator> PromotionUserCreateDTOValidatorMock { get; }

        public PromotionUserServiceConfiguration()
        {
            PromotionUserRepositoryMock = new();
            MapperMock = new();
            UnitOfWorkMock = new();
            PromotionUserCreateDTOValidatorMock = new();
        }
    }
}
