using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PrintWayy.SpotWayy.Entities;
using PrintWayy.SpotWayy.SpotWayyApp.Models;

namespace PrintWayy.SpotWayy.SpotWayyApp.Mapping
{
    public class DomainToViewModelMappingProfile : Profile
    {    
        protected override void Configure()
        {
            Mapper.CreateMap<AlbumVO, AlbumModel>();
            Mapper.CreateMap<MusicVO, MusicModel>();
        }
    }
}