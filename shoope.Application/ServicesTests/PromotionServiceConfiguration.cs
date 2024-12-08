using AutoMapper;
using Moq;
using Shoope.Application.DTOs.Validations.Interfaces;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.UtilityExternal.Interface;

namespace Shoope.Application.ServicesTests
{
    public class PromotionServiceConfiguration
    {
        public Mock<IPromotionRepository> PromotionRepositoryMock { get; }
        public Mock<IPromotionUserService> PromotionUserServiceMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }
        public Mock<ICloudinaryUti> CloudinaryUtiMock { get; }
        public Mock<IPromotionCreateDTOValidator> PromotionCreateDTOValidatorMock { get; }
        public Mock<IPromotionCreateDTOIfPromotionNumber2Validator> PromotionCreateDTOIfPromotionNumber2ValidatorMock { get; }

        public PromotionServiceConfiguration()
        {
            PromotionRepositoryMock = new();
            PromotionUserServiceMock = new();
            MapperMock = new();
            UnitOfWorkMock = new();
            CloudinaryUtiMock = new();
            PromotionCreateDTOValidatorMock = new();
            PromotionCreateDTOIfPromotionNumber2ValidatorMock = new();
        }
    }
}
