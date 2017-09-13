using AutoMapper;
using MoodPocket.Domain.Entities;
using MoodPocket.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoodPocket.WebUI.Infrastructure.AutoMapperProfiles
{
    public class MemeAutoMapperProfile : Profile
    {
        public MemeAutoMapperProfile()
        {
            CreateMap<Picture, PictureModel>()
                .ForMember(dest => dest.Url, opts => opts.MapFrom(src => src.Url));
        }
    }
}