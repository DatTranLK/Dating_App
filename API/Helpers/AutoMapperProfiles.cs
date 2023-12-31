using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(obj => obj.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(obj => obj.Photos))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(obj => DateTimeExtensions.CalculateAge(obj.DateOfBirth)));
            CreateMap<Photo, PhotoDto>();
        }
    }
}