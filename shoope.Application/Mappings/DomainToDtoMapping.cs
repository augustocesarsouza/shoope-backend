using AutoMapper;
using Shoope.Application.DTOs;
using Shoope.Domain.Entities;

namespace Shoope.Application.Mappings
{
    public class DomainToDtoMapping : Profile
    {
        public DomainToDtoMapping() 
        {
            CreateMap<User, UserDTO>();
            //.ForMember(x => x.PasswordHash, opt => opt.Ignore());
            //CreateMap<Address, AddressDTO>();

            CreateMap<Address, AddressDTO>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    srcMember != null && !(srcMember is string str && string.IsNullOrEmpty(str))
                ));
        }
    }
}
