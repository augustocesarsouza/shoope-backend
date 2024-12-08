using AutoMapper;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Shoope.Application.DTOs;
using Shoope.Application.DTOs.Validations.Interfaces;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.UtilityExternal.Interface;
using Shoope.Infra.Data.CloudinaryConfigClass;

namespace Shoope.Application.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly IPromotionUserService _promotionUserService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryUti _cloudinaryUti;
        private readonly IPromotionCreateDTOValidator _promotionCreateDTOValidator;
        private readonly IPromotionCreateDTOIfPromotionNumber2Validator _promotionCreateDTOIfPromotionNumber2Validator;
        
        private readonly Account _account = new Account(
            CloudinaryConfig.AccountName,
            CloudinaryConfig.ApiKey,
            CloudinaryConfig.ApiSecret);

        public PromotionService(IPromotionRepository promotionRepository, IPromotionUserService promotionUserService,
            IMapper mapper, IUnitOfWork unitOfWork, ICloudinaryUti cloudinaryUti, IPromotionCreateDTOValidator promotionCreateDTOValidator,
            IPromotionCreateDTOIfPromotionNumber2Validator promotionCreateDTOIfPromotionNumber2Validator)
        {
            _promotionRepository = promotionRepository;
            _promotionUserService = promotionUserService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cloudinaryUti = cloudinaryUti;
            _promotionCreateDTOValidator = promotionCreateDTOValidator;
            _promotionCreateDTOIfPromotionNumber2Validator = promotionCreateDTOIfPromotionNumber2Validator;
        }

        public async Task<ResultService<PromotionDTO>> GetById(Guid promotionId)
        {
            try
            {
                var getById = await _promotionRepository.GetById(promotionId);

                return ResultService.Ok(_mapper.Map<PromotionDTO>(getById));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<PromotionDTO>(ex.Message);
            }
        }

        public async Task<ResultService<PromotionDTO>> Create(PromotionDTO? promotionDTO)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                if (promotionDTO == null)
                    return ResultService.Fail<PromotionDTO>("error DTO informed is Null");

                if (promotionDTO.DateCreate == null)
                    return ResultService.Fail<PromotionDTO>("error DateCreate is null");

                // 05/10/1999 13:50
                //360 × 360

                var validateDTO = _promotionCreateDTOValidator.ValidateDTO(promotionDTO);

                if (!validateDTO.IsValid)
                    return ResultService.RequestError<PromotionDTO>("validation error check the information", validateDTO);

                if(promotionDTO.WhatIsThePromotion == 2)
                {
                    var validateDTOPromotionNumberTwo = _promotionCreateDTOIfPromotionNumber2Validator.ValidateDTO(promotionDTO);

                    if (!validateDTOPromotionNumberTwo.IsValid)
                        return ResultService.RequestError<PromotionDTO>("validation error check the information", validateDTOPromotionNumberTwo);
                }

                CloudinaryCreate result = new();

                CloudinaryCreate resultCreateImgFirst = new();
                CloudinaryCreate resultCreateImgSecond = new();
                CloudinaryCreate resultCreateImgThird = new();

                var promotionId = Guid.NewGuid();

                Promotion? Promotion = null;

                var stringCortada = promotionDTO.DateCreate.Split(' ');
                var stringCortadaDayMonthYear = stringCortada[0];
                var stringHourMin = stringCortada[1];

                var stringCortadaDayMonthYearSplit = stringCortadaDayMonthYear.Split("/");
                var stringHourMinSplit = stringHourMin.Split(":");

                var dia = stringCortadaDayMonthYearSplit[0];
                var mes = stringCortadaDayMonthYearSplit[1];
                var ano = stringCortadaDayMonthYearSplit[2];

                var hora = stringHourMinSplit[0];
                var min = stringHourMinSplit[1];

                var birthDate = new DateTime(int.Parse(ano), int.Parse(mes), int.Parse(dia), int.Parse(hora), int.Parse(min), 0);
                var birthDateUtc = DateTime.SpecifyKind(birthDate, DateTimeKind.Utc);

                if (promotionDTO.WhatIsThePromotion == 1)
                {
                    result = await _cloudinaryUti.CreateMedia(promotionDTO.Img, "img-shopee", 360, 360);

                    if (result.ImgUrl == null || result.PublicId == null)
                        return ResultService.Fail<PromotionDTO>("erro ao criar imgMain");

                   Promotion = new Promotion(promotionId, promotionDTO.WhatIsThePromotion, promotionDTO.Title,
                   promotionDTO.Description, birthDateUtc, result.ImgUrl, result.PublicId, promotionDTO.ImgInnerFirst, promotionDTO.AltImgInnerFirst,
                   promotionDTO.ImgInnerSecond, promotionDTO.AltImgInnerSecond, promotionDTO.ImgInnerThird, promotionDTO.AltImgInnerThird, null, null, null);

                }
                else if (promotionDTO.WhatIsThePromotion == 2)
                {
                    result = await _cloudinaryUti.CreateMedia(promotionDTO.Img, "img-shopee", 360, 360);

                    if (result.ImgUrl == null || result.PublicId == null)
                        return ResultService.Fail<PromotionDTO>("erro ao criar imgMain");

                    if (promotionDTO.ImgInnerFirst != null)
                    {
                        resultCreateImgFirst = await _cloudinaryUti.CreateMedia(promotionDTO.ImgInnerFirst, "img-shopee", 360, 360);
                    }

                    if(promotionDTO.ImgInnerSecond != null)
                    {
                        resultCreateImgSecond = await _cloudinaryUti.CreateMedia(promotionDTO.ImgInnerSecond, "img-shopee", 360, 360);
                    }

                    if (promotionDTO.ImgInnerThird != null)
                    {
                        resultCreateImgThird = await _cloudinaryUti.CreateMedia(promotionDTO.ImgInnerThird, "img-shopee", 360, 360);
                    }

                     Promotion = new Promotion(promotionId, promotionDTO.WhatIsThePromotion, promotionDTO.Title,
                     promotionDTO.Description, birthDateUtc, result.ImgUrl, result.PublicId, resultCreateImgFirst.ImgUrl, promotionDTO.AltImgInnerFirst,
                     resultCreateImgSecond.ImgUrl, promotionDTO.AltImgInnerSecond, resultCreateImgThird.ImgUrl, promotionDTO.AltImgInnerThird,
                     resultCreateImgFirst.PublicId, resultCreateImgSecond.PublicId, resultCreateImgThird.PublicId);
                }

                //var promotionId = Guid.NewGuid();

                //var Promotion = new Promotion(promotionId, promotionDTO.WhatIsThePromotion, promotionDTO.Title,
                //    promotionDTO.Description, birthDateUtc, result.ImgUrl, result.PublicId, promotionDTO.ImgInnerFirst, promotionDTO.AltImgInnerFirst,
                //    promotionDTO.ImgInnerSecond, promotionDTO.AltImgInnerSecond, promotionDTO.ImgInnerThird, promotionDTO.AltImgInnerThird);

                //Promotion.PublicId = result.PublicId;

                if (Promotion == null)
                    return ResultService.Fail<PromotionDTO>("error promotion is null");

                var createPromotion = await _promotionRepository.CreateAsync(Promotion);

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<PromotionDTO>(createPromotion));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<PromotionDTO>(ex.Message);
            }
        }

        public async Task<ResultService<PromotionDTO>> DeletePromotion(Guid promotionId)
        {
            var promotionDelete = await _promotionRepository.GetById(promotionId);

            if (promotionDelete == null)
                return ResultService.Fail<PromotionDTO>("error promotion not found");

            var deletePromotion = await _promotionUserService.Delete(promotionId);

            if (!deletePromotion.IsSucess)
                return ResultService.Fail<PromotionDTO>(deletePromotion.Message ?? "error delete");

            var cloudinary = new Cloudinary(_account);

            try
            {
                await _unitOfWork.BeginTransaction();
                var deleteMovie = await _promotionRepository.DeleteAsync(promotionDelete);

                if (deleteMovie.PublicIdImg != null && deleteMovie.WhatIsThePromotion == 1)
                {
                    // TEM QUE TESTAR DELETAR PORQUE ESTÁ DANDO ERRADO "PublicId"
                    var destroyParams = new DeletionParams(deleteMovie.PublicIdImg) { ResourceType = ResourceType.Image };
                    await cloudinary.DestroyAsync(destroyParams);
                }

                if (deleteMovie.PublicIdImg != null || deleteMovie.WhatIsThePromotion == 2)
                {
                    if(deleteMovie.ImgInnerFirstPublicId != null && deleteMovie.ImgInnerSecondPublicId != null && deleteMovie.ImgInnerThirdPublicId != null)
                    {
                        var destroyParams = new DeletionParams(deleteMovie.PublicIdImg) { ResourceType = ResourceType.Image };
                        await cloudinary.DestroyAsync(destroyParams);

                        var destroyParamsFirst = new DeletionParams(deleteMovie.ImgInnerFirstPublicId) { ResourceType = ResourceType.Image };
                        await cloudinary.DestroyAsync(destroyParamsFirst);

                        var destroyParamsSecond = new DeletionParams(deleteMovie.ImgInnerSecondPublicId) { ResourceType = ResourceType.Image };
                        await cloudinary.DestroyAsync(destroyParamsSecond);

                        var destroyParamsThird = new DeletionParams(deleteMovie.ImgInnerThirdPublicId) { ResourceType = ResourceType.Image };
                        await cloudinary.DestroyAsync(destroyParamsThird);
                    }
                }

                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<PromotionDTO>(deleteMovie));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<PromotionDTO>($"{ex.Message}");
            }
        }
    }
}
