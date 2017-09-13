using AutoMapper;
using MoodPocket.WebUI.Infrastructure.AutoMapperProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoodPocket.WebUI.App_Start
{
    public static class MapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(
                cfg =>
                {
                    cfg.AddProfile<UserAutoMapperProfile>();
                    
                }
            );
        }
    }
}