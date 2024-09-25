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
        }
    }
}
