using Moq;
using Shoope.Application.DTOs;
using Shoope.Application.Services;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Shoope.Infra.Data.CloudinaryConfigClass;
using Xunit;

namespace Shoope.Application.ServicesTests.CategoriesServiceTest
{
    public class CategoriesServiceTest
    {
        private readonly CategoriesServiceConfiguration _categoriesServiceConfiguration;
        private readonly CategoriesService _categoriesService;

        public CategoriesServiceTest()
        {
            _categoriesServiceConfiguration = new();
            var categoriesService = new CategoriesService(_categoriesServiceConfiguration.CategoriesRepositoryMock.Object,
                _categoriesServiceConfiguration.MapperMock.Object, _categoriesServiceConfiguration.UnitOfWorkMock.Object,
                _categoriesServiceConfiguration.CloudinaryUtiMock.Object);

            _categoriesService = categoriesService;
        }

        [Fact]
        public async Task Should_GetCategoriesById_Success()
        {
            Guid addressId = Guid.NewGuid();

            _categoriesServiceConfiguration.CategoriesRepositoryMock.Setup(rep => rep.GetCategoriesById(It.IsAny<Guid>()))
                .ReturnsAsync(new Categories());

            var result = await _categoriesService.GetCategoriesById(addressId);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_GetCategoriesById()
        {
            Guid addressId = Guid.NewGuid();

            _categoriesServiceConfiguration.CategoriesRepositoryMock.Setup(rep => rep.GetCategoriesById(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("Erro ao buscar endereço"));

            var result = await _categoriesService.GetCategoriesById(addressId);

            Assert.False(result.IsSucess);
            Assert.Equal("Erro ao buscar endereço", result.Message);
        }

        [Fact]
        public async Task Should_GetAllCategories_Success()
        {
            Guid addressId = Guid.NewGuid();

            _categoriesServiceConfiguration.CategoriesRepositoryMock.Setup(rep => rep.GetAllCategories())
                .ReturnsAsync(new List<Categories>());

            var result = await _categoriesService.GetAllCategories();
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_GetAllCategories()
        {
            Guid addressId = Guid.NewGuid();

            _categoriesServiceConfiguration.CategoriesRepositoryMock.Setup(rep => rep.GetAllCategories())
                .ThrowsAsync(new Exception("Erro ao buscar endereço"));

            var result = await _categoriesService.GetAllCategories();

            Assert.False(result.IsSucess);
            Assert.Equal("Erro ao buscar endereço", result.Message);
        }

        [Fact]
        public async Task Should_CreateAsync_Success()
        {
            var categoryId = Guid.NewGuid();
            CategoriesDTO categoriesDTO = new CategoriesDTO(categoryId, "http://res.cloudinary.com/dyqsqg7pk/image/upload/v1/category-all/rnznrjsikuwt66cbkhva", 
                "category-all/rnznrjsikuwt66cbkhva", "category-women-clothing", "Roupas Femininas");

            //_movieServiceConfiguration.MovieDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<MovieDTO>())).Returns(new ValidationResult());

            var cloudinary = new CloudinaryCreate
            {
                ImgUrl = "http://res.cloudinary.com/dyqsqg7pk/image/upload/pd6iwh7kprpcrauaiao1",
                PublicId = "ascascascas"
            };

            _categoriesServiceConfiguration.CloudinaryUtiMock.Setup(cloud => cloud.CreateMedia(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(cloudinary);

            _categoriesServiceConfiguration.CategoriesRepositoryMock.Setup(rep => rep.CreateAsync(It.IsAny<Categories>()))
                .ReturnsAsync(new Categories());

            var result = await _categoriesService.CreateAsync(categoriesDTO);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Throw_Error_When_Create_DTO_Is_Null()
        {
            var result = await _categoriesService.CreateAsync(null);
            Assert.False(result.IsSucess);
            Assert.Equal("error DTO is null", result.Message);
        }

        [Fact]
        public async Task Should_Throw_Error_When_Create_ImgCategory_Is_Null()
        {
            var categoryId = Guid.NewGuid();
            CategoriesDTO categoriesDTO = new CategoriesDTO(categoryId, null,
                "category-all/rnznrjsikuwt66cbkhva", "category-women-clothing", "Roupas Femininas");

            //_movieServiceConfiguration.MovieDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<MovieDTO>())).Returns(new ValidationResult());

            var result = await _categoriesService.CreateAsync(categoriesDTO);
            Assert.False(result.IsSucess);
            Assert.Equal("Error img product must be informed", result.Message);
        }

        [Fact]
        public async Task Should_Throw_Error_When_Create_ThrowException()
        {
            var categoryId = Guid.NewGuid();
            CategoriesDTO categoriesDTO = new CategoriesDTO(categoryId, "http://res.cloudinary.com/dyqsqg7pk/image/upload/v1/category-all/rnznrjsikuwt66cbkhva",
                "category-all/rnznrjsikuwt66cbkhva", "category-women-clothing", "Roupas Femininas");

            //_movieServiceConfiguration.MovieDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<MovieDTO>())).Returns(new ValidationResult());

            var cloudinary = new CloudinaryCreate
            {
                ImgUrl = "http://res.cloudinary.com/dyqsqg7pk/image/upload/pd6iwh7kprpcrauaiao1",
                PublicId = "ascascascas"
            };

            _categoriesServiceConfiguration.CloudinaryUtiMock.Setup(cloud => cloud.CreateMedia(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(cloudinary);

            _categoriesServiceConfiguration.CategoriesRepositoryMock.Setup(rep => rep.CreateAsync(It.IsAny<Categories>()))
                .ThrowsAsync(new Exception("Erro ao criar categories"));

            var result = await _categoriesService.CreateAsync(categoriesDTO);
            Assert.False(result.IsSucess);
            Assert.Equal("Erro ao criar categories", result.Message);
        }
    }
}

//_movieServiceConfiguration.MovieDTOValidatorMock.Setup(valid => valid.ValidateDTO(It.IsAny<MovieDTO>()))
//                .Returns(new ValidationResult(new List<ValidationFailure>
//                {
//                new ValidationFailure("PropertyName", "Error message 1"),
//                }));