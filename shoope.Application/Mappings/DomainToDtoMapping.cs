using AutoMapper;
using Shoope.Application.DTOs;
using Shoope.Domain.Entities;

namespace Shoope.Application.Mappings
{
    public class DomainToDtoMapping : Profile
    {
        public DomainToDtoMapping() 
        {
            CreateMap<User, UserDTO>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    srcMember != null && !(srcMember is string str && string.IsNullOrEmpty(str))
                ));

            //.ForMember(x => x.PasswordHash, opt => opt.Ignore());
            //CreateMap<Address, AddressDTO>();

            CreateMap<Address, AddressDTO>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    srcMember != null && !(srcMember is string str && string.IsNullOrEmpty(str))
                ));

            CreateMap<Promotion, PromotionDTO>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    srcMember != null && !(srcMember is string str && string.IsNullOrEmpty(str))
                ));

            //CreateMap<Promotion, PromotionDTO>();

            CreateMap<PromotionUser, PromotionUserDTO>()
                .ForMember(dest => dest.PromotionDTO, opt => opt.MapFrom(src => src.Promotion))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    srcMember != null &&
                    (!(srcMember is string) || !string.IsNullOrEmpty((string)srcMember))
                ));

            CreateMap<Cupon, CuponDTO>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    srcMember != null && !(srcMember is string str && string.IsNullOrEmpty(str))
                ));

            CreateMap<UserCupon, UserCuponDTO>()
                .ForMember(dest => dest.CuponDTO, opt => opt.MapFrom(src => src.Cupon))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    srcMember != null && !(srcMember is string str && string.IsNullOrEmpty(str))
                ));

            //CreateMap<PromotionUser, PromotionUserDTO>();

            //CreateMap<PromotionUser, PromotionUserDTO>()
            //.ForMember(dest => dest.PromotionDTO, opt => opt.MapFrom(src => src.Promotion));
            //.ForMember(x => x.CinemaId, opt => opt.Ignore())
            //.ForMember(x => x.Id, opt => opt.Ignore())
            //.ForMember(x => x.MovieId, opt => opt.Ignore())
            //.ForMember(x => x.RegionId, opt => opt.Ignore());
        }
    }
}
