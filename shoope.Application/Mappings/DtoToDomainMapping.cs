using AutoMapper;
using Shoope.Application.DTOs;
using Shoope.Domain.Entities;

namespace Shoope.Application.Mappings
{
    public class DtoToDomainMapping : Profile
    {
        public DtoToDomainMapping() 
        {
            CreateMap<UserDTO, User>();
            CreateMap<AddressDTO, Address>();
            CreateMap<PromotionDTO, Promotion>();
            CreateMap<PromotionUserDTO, PromotionUser>();
            CreateMap<CuponDTO, Cupon>();
            CreateMap<UserCuponDTO, UserCupon>();
            CreateMap<ProductsOfferFlashDTO, ProductsOfferFlash>();
            CreateMap<CategoriesDTO, Categories>();
            CreateMap<ProductHighlightDTO, ProductHighlight>();
            CreateMap<ProductDiscoveriesOfDayDTO, ProductDiscoveriesOfDay>();
            CreateMap<FlashSaleProductAllInfoDTO, FlashSaleProductAllInfo>();
            CreateMap<ProductFlashSaleReviewsDTO, ProductFlashSaleReviews>();
            CreateMap<UserSellerProductDTO, UserSellerProduct>();
            CreateMap<ProductSellerDTO, ProductSeller>();
            CreateMap<ProductOptionImageDTO, ProductOptionImage>();
            CreateMap<ProductDetailDTO, ProductDetail>();
            CreateMap<ProductDescriptionDTO, ProductDescription>();
            CreateMap<LikeReviewDTO, LikeReview>();
        }
    }
}