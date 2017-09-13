using AutoMapper;
using MoodPocket.Domain.Entities;
using MoodPocket.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoodPocket.WebUI.Infrastructure.AutoMapperProfiles
{
    public class UserAutoMapperProfile : Profile
    {
        public UserAutoMapperProfile()
        {
            CreateMap<RegisterModel, User>()
                .ForMember( dest=>dest.Id, opts=>opts.Ignore())
                .ForMember( dest => dest.Gallery,
                            opts => opts.MapFrom(src => src.Gallery ?? null));

            CreateMap<User, UserCard>()
                .ForMember( dest => dest.User, opts => opts.MapFrom(src => src))
                .ForMember( dest => dest.Gallery, opts => opts.MapFrom(src => src.Gallery))
                .ForMember( dest => dest.SavedMemesCount, 
                            opts => opts.MapFrom(src => src.Gallery.GalleryPictures.Count))
                .ForMember( dest => dest.BackgroundPictureUrl, 
                            opts => opts.MapFrom(src => src.Gallery.GalleryPictures.First().Picture.Url));
        }
    }
}