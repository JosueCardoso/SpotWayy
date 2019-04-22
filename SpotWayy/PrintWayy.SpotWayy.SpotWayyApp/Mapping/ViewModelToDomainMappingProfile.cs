using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PrintWayy.SpotWayy.Entities;
using PrintWayy.SpotWayy.SpotWayyApp.Models;

namespace PrintWayy.SpotWayy.SpotWayyApp.Mapping
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<AlbumModel, AlbumVO>();
            Mapper.CreateMap<MusicModel, MusicVO>();
        }
    }
}