using AutoMapper;
using Spotters.Data.Models;
using Spotters.Data.Repository;
using Spotters.Data.UnitOfWork;
using Spotters.Service.Interface;
using Spotters.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spotters.Service.Implementation
{
    public class SpotterService: BaseService<PlaneSpotterViewModel, PlaneSpotter>, ISpotterService
    {
        public  SpotterService(IMapper autoMapper, IUnitOfWork uow, IBaseRepository<PlaneSpotter> repo)
            :base(autoMapper, uow, repo)
        {

        }
    }
}
