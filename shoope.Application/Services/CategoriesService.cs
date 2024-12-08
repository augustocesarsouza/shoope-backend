using AutoMapper;
using Shoope.Application.DTOs;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.CloudinaryConfigClass;
using Shoope.Infra.Data.UtilityExternal.Interface;

namespace Shoope.Application.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryUti _cloudinaryUti;

        public CategoriesService(ICategoriesRepository categoriesRepository, IMapper mapper, IUnitOfWork unitOfWork, ICloudinaryUti cloudinaryUti)
        {
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cloudinaryUti = cloudinaryUti;
        }

        public async Task<ResultService<CategoriesDTO>> GetCategoriesById(Guid categorieId)
        {
            try
            {
                var categoryById = await _categoriesRepository.GetCategoriesById(categorieId);

                return ResultService.Ok(_mapper.Map<CategoriesDTO>(categoryById));

            }catch (Exception ex)
            {
                return ResultService.Fail<CategoriesDTO>(ex.Message);
            }
        }

        public async Task<ResultService<List<CategoriesDTO>>> GetAllCategories()
        {
            try
            {
                var categories = await _categoriesRepository.GetAllCategories();

                return ResultService.Ok(_mapper.Map<List<CategoriesDTO>>(categories));

            }
            catch (Exception ex)
            {
                return ResultService.Fail<List<CategoriesDTO>>(ex.Message);
            }
        }

        public async Task<ResultService<CategoriesDTO>> CreateAsync(CategoriesDTO? categoriesDTO)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                if (categoriesDTO == null)
                    return ResultService.Fail<CategoriesDTO>("error DTO is null");

                CloudinaryCreate result = new();

                if (categoriesDTO.ImgCategory == null)
                    return ResultService.Fail<CategoriesDTO>("Error img product must be informed");

                result = await _cloudinaryUti.CreateMedia(categoriesDTO.ImgCategory, "category-all", 244, 244);

                if (result.ImgUrl == null || result.PublicId == null)
                    return ResultService.Fail<CategoriesDTO>("error when create ImgProduct");

                categoriesDTO.SetImgCategory(result.ImgUrl);
                categoriesDTO.SetImgCategoryPublicId(result.PublicId);

                var id = Guid.NewGuid();

                categoriesDTO.SetId(id);

                var productCreate = await _categoriesRepository.CreateAsync(_mapper.Map<Categories>(categoriesDTO));

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<CategoriesDTO>(productCreate));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<CategoriesDTO>(ex.Message);
            }
        }
    }
}
