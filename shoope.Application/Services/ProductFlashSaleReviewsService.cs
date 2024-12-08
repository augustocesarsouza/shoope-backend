using AutoMapper;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Shoope.Application.DTOs;
using Shoope.Application.DTOs.Validations.Interfaces;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.CloudinaryConfigClass;
using Shoope.Infra.Data.UtilityExternal.Interface;

using SixLabors.ImageSharp;
using Shoope.Infra.Data;

namespace Shoope.Application.Services
{
    public class ProductFlashSaleReviewsService : IProductFlashSaleReviewsService
    {
        private readonly IProductFlashSaleReviewsRepository _productFlashSaleReviewsRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductFlashSaleReviewsCreateDTOValidator _productFlashSaleReviewsCreateDTOValidator;
        private readonly ICloudinaryUti _cloudinaryUti;

        private readonly Account _account = new Account(
            CloudinaryConfig.AccountName,
            CloudinaryConfig.ApiKey,
            CloudinaryConfig.ApiSecret);

        public ProductFlashSaleReviewsService(IProductFlashSaleReviewsRepository productFlashSaleReviewsRepository, IMapper mapper, IUnitOfWork unitOfWork,
            IProductFlashSaleReviewsCreateDTOValidator productFlashSaleReviewsCreateDTOValidator, ICloudinaryUti cloudinaryUti)
        {
            _productFlashSaleReviewsRepository = productFlashSaleReviewsRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productFlashSaleReviewsCreateDTOValidator = productFlashSaleReviewsCreateDTOValidator;
            _cloudinaryUti = cloudinaryUti;
        }

        public async Task<ResultService<List<ProductFlashSaleReviewsDTO>?>> GetAllProductFlashSaleReviewsByProductFlashSaleId(Guid productFlashSaleId)
        {
            try
            {
                var productFlashSale = await _productFlashSaleReviewsRepository.GetAllProductFlashSaleReviewsByProductFlashSaleId(productFlashSaleId);

                return ResultService.Ok(_mapper.Map<List<ProductFlashSaleReviewsDTO>?>(productFlashSale));

            }
            catch (Exception ex)
            {
                return ResultService.Fail<List<ProductFlashSaleReviewsDTO>?>(ex.Message);
            }
        }

        public async Task<ResultService<ProductFlashSaleReviewsDTO>> CreateAsync(ProductFlashSaleReviewsDTO? productFlashSaleReviewsDTO)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                if (productFlashSaleReviewsDTO == null)
                    return ResultService.Fail<ProductFlashSaleReviewsDTO>("error DTO is null");

                var validatorDTO = _productFlashSaleReviewsCreateDTOValidator.ValidateDTO(productFlashSaleReviewsDTO);

                if (!validatorDTO.IsValid)
                    return ResultService.RequestError<ProductFlashSaleReviewsDTO>("validation error check the information", validatorDTO);

                var id = Guid.NewGuid();
                productFlashSaleReviewsDTO.SetId(id);

                DateTime creationDate = DateTime.UtcNow;

                productFlashSaleReviewsDTO.SetCreationDate(creationDate);
                var imgAndVideoReviewsProductList = new List<string>();

                if (productFlashSaleReviewsDTO.ImgAndVideoReviewsProductElements != null)
                {
                    foreach (var el in productFlashSaleReviewsDTO.ImgAndVideoReviewsProductElements)
                    {
                        bool isImage = el.StartsWith("data:image");
                        bool isVideo = el.StartsWith("data:video");

                        if (isImage)
                        {
                            string base64String = el.Split(',')[1];
                            var width = 10;
                            var height = 10;

                            // Converte a string base64 em um array de bytes
                            byte[] imageBytes = Convert.FromBase64String(base64String);

                            // Usa a classe Image para carregar a imagem a partir do array de bytes
                            using (var ms = new MemoryStream(imageBytes))
                            {
                                using (var image = Image.Load(ms))
                                {
                                    width = image.Width;
                                    height = image.Height; // criar as imagens no cloudinary e banco de dados
                                }
                            }

                            CloudinaryCreate result = await _cloudinaryUti.CreateMedia(el, "reviews-product-flash-sale-img-and-video", width, height);

                            if (result.ImgUrl == null || result.PublicId == null)
                            {
                                await _unitOfWork.Rollback();
                                return ResultService.Fail<ProductFlashSaleReviewsDTO>("error when create ImgPerfil");
                            }

                            imgAndVideoReviewsProductList.Add(result.ImgUrl);

                        }
                        else if(isVideo)
                        {
                            CloudinaryCreate result = await _cloudinaryUti.CreateMedia(el, "reviews-product-flash-sale-img-and-video", 517, 919);

                            if (result.ImgUrl == null || result.PublicId == null)
                            {
                                await _unitOfWork.Rollback();
                                return ResultService.Fail<ProductFlashSaleReviewsDTO>("error when create ImgPerfil");
                            }

                            imgAndVideoReviewsProductList.Add(result.ImgUrl);
                        }
                    }
                }

                productFlashSaleReviewsDTO.SetImgAndVideoReviewsProduct(imgAndVideoReviewsProductList);

                //result = await _cloudinaryUti.CreateImg(userSellerProductDTO.ImgPerfilBase64, "reviews-product-flash-sale-img-and-video", 517, 80);

                //if (result.ImgUrl == null || result.PublicId == null)
                //    return ResultService.Fail<UserSellerProductDTO>("error when create ImgPerfil");

                ProductFlashSaleReviews flashSaleProductAllInfo = _mapper.Map<ProductFlashSaleReviews>(productFlashSaleReviewsDTO);

                var productCreate = await _productFlashSaleReviewsRepository.CreateAsync(flashSaleProductAllInfo);

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<ProductFlashSaleReviewsDTO>(productCreate));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<ProductFlashSaleReviewsDTO>(ex.Message);
            }
        }

        public async Task<ResultService<ProductFlashSaleReviewsDTO>> Delete(Guid productFlashSaleReviewsId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var promotionDelete = await _productFlashSaleReviewsRepository.GetByProductFlashSaleId(productFlashSaleReviewsId);

                if (promotionDelete == null)
                    return ResultService.Fail<ProductFlashSaleReviewsDTO>("error promotion not found");

                Cloudinary cloudinary = new Cloudinary(_account);

                var deleteMovie = await _productFlashSaleReviewsRepository.DeleteAsync(promotionDelete);

                if (deleteMovie.ImgAndVideoReviewsProduct == null)
                    return ResultService.Fail<ProductFlashSaleReviewsDTO>("error ImgAndVideoReviewsProduct is null");

                foreach (var el in deleteMovie.ImgAndVideoReviewsProduct)
                {
                    string url = el;
                    string startPattern = "/reviews-product-flash-sale-img-and-video/";

                    int startIndex = url.IndexOf(startPattern);

                    bool isImage = url.Contains("/image/");
                    bool isVideo = url.Contains("/video/");

                    if (startIndex != -1)
                    {
                        string result = url.Substring(startIndex + startPattern.Length);
                        result = "reviews-product-flash-sale-img-and-video/" + result; // Reanexando a parte inicial desejada

                        if (isImage)
                        {
                            CloudinaryResult cloudinaryResult = _cloudinaryUti.DeleteMediaCloudinary(result, ResourceType.Image, cloudinary);

                            if (!cloudinaryResult.DeleteSuccessfully)
                                return ResultService.Fail<ProductFlashSaleReviewsDTO>("error when delete image");
                        }
                        else if (isVideo)
                        {
                            CloudinaryResult cloudinaryResult = _cloudinaryUti.DeleteMediaCloudinary(result, ResourceType.Image, cloudinary);

                            if (!cloudinaryResult.DeleteSuccessfully)
                                return ResultService.Fail<ProductFlashSaleReviewsDTO>("error when delete video");
                        }
                    }
                }

                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<ProductFlashSaleReviewsDTO>(deleteMovie));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<ProductFlashSaleReviewsDTO>($"{ex.Message}");
            }
        }
    }
}
