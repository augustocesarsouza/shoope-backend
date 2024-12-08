using AutoMapper;
using Shoope.Application.DTOs;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.CloudinaryConfigClass;
using Shoope.Infra.Data.UtilityExternal.Interface;

namespace Shoope.Application.Services
{
    public class UserSellerProductService : IUserSellerProductService
    {
        private readonly IUserSellerProductRepository _userSellerProductRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryUti _cloudinaryUti;

        public UserSellerProductService(IUserSellerProductRepository userSellerProductRepository, IMapper mapper, IUnitOfWork unitOfWork, ICloudinaryUti cloudinaryUti)
        {
            _userSellerProductRepository = userSellerProductRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cloudinaryUti = cloudinaryUti;
        }

        public async Task<ResultService<UserSellerProductDTO>> GetById(Guid userSellerProductId)
        {
            try
            {
                var userSeller = await _userSellerProductRepository.GetById(userSellerProductId);

                return ResultService.Ok(_mapper.Map<UserSellerProductDTO>(userSeller));

            }
            catch (Exception ex)
            {
                return ResultService.Fail<UserSellerProductDTO>(ex.Message);
            }
        }

        public async Task<ResultService<UserSellerProductDTO>> Create(UserSellerProductDTO userSellerProductDTO)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                if (userSellerProductDTO == null)
                    return ResultService.Fail<UserSellerProductDTO>("error DTO informed is Null");

                CloudinaryCreate result = new();

                var userSellerProductId = Guid.NewGuid();

                DateTime creationDate = DateTime.UtcNow;

                if (userSellerProductDTO.ImgPerfilBase64 == null)
                    return ResultService.Fail<UserSellerProductDTO>("error ImgPerfilBase64 Is null");

                result = await _cloudinaryUti.CreateMedia(userSellerProductDTO.ImgPerfilBase64, "img-user-seller", 80, 80);

                if (result.ImgUrl == null || result.PublicId == null)
                    return ResultService.Fail<UserSellerProductDTO>("error when create ImgPerfil");

                CloudinaryCreate resultImgFloating = new();

                if (userSellerProductDTO.ImgFloatingBase64 != null)
                {
                    resultImgFloating = await _cloudinaryUti.CreateMedia(userSellerProductDTO.ImgFloatingBase64, "img-user-seller", 197, 48);

                    if (resultImgFloating.ImgUrl == null || resultImgFloating.PublicId == null)
                        return ResultService.Fail<UserSellerProductDTO>("error when create ImgFloating");
                }

                userSellerProductDTO.SetImgPerfil(result.ImgUrl);
                userSellerProductDTO.SetImgPerfilPublicId(result.PublicId);

                var userSellerProduct = new UserSellerProduct(userSellerProductId, userSellerProductDTO.Name, userSellerProductDTO.ImgPerfil,
                    userSellerProductDTO.ImgPerfilPublicId, resultImgFloating.ImgUrl, resultImgFloating.PublicId, creationDate, userSellerProductDTO.Reviews, userSellerProductDTO.ChatResponseRate,
                    creationDate, userSellerProductDTO.QuantityOfProductSold, userSellerProductDTO.UsuallyRespondsToChatIn, userSellerProductDTO.Followers);

                if (userSellerProduct == null)
                    return ResultService.Fail<UserSellerProductDTO>("error when map");

                var createPromotion = await _userSellerProductRepository.CreateAsync(userSellerProduct);

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<UserSellerProductDTO>(createPromotion));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<UserSellerProductDTO>(ex.Message);
            }
        }
    }
}
