using AutoMapper;
using Models.Domain;
using Models.DTOs.Auth;
using Models.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterRequestDTO, ApplicationUser>()
                .ForMember(dest => dest.Skills, opt => opt.MapFrom(src =>
                    src.Skills != null ? string.Join(",", src.Skills) : null
                ));
        }
    }
}
