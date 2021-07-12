using AutoMapper;
using Spotters.Data.Models;
using Spotters.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spotters.Service.Mapping
{
    public class TransformationDataMappingProfile : Profile
    {
        public TransformationDataMappingProfile()
        {
            CreateMap<PlaneSpotter, PlaneSpotterViewModel>()
                .ReverseMap();
        }
    }
}
