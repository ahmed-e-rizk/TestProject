using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Entites;
using Web.DTO.Auth;
using Web.DTO.Auth.Setting;

namespace Web.BLL.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RegisterDto, User>()
                .ForMember(e => e.Email, o => o.MapFrom(e => e.Email))
                .ForMember(e => e.Name, o => o.MapFrom(e => e.Name))
                .ForMember(e => e.DateOfbirth, o => o.MapFrom(e => e.DateOfbirth))
                .ForMember(e => e.Addresses, o => o.MapFrom(e => e.Addresses))
                .ForMember(e => e.Gender, o => o.MapFrom(e => e.Gender))
                .ForMember(e => e.Phones, o => o.MapFrom(e => e.Phones))
                .ForMember(e => e.Password, o => o.MapFrom(e => e.Password))
              
                ;


            CreateMap<AddressDto, Address>()
                .ForMember(e => e.Address1, o => o.MapFrom(e => e.Address))
                ;
            CreateMap< PhoneDto, Phone>()
                                .ForMember(e => e.Number, o => o.MapFrom(e => e.Phone))

                ;
            CreateMap<RefreshTokenDto, RefreshToken>()
                ;
        }
    }
}
