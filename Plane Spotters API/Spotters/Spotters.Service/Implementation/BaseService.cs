using AutoMapper;
using Spotters.Data.Models;
using Spotters.Data.Repository;
using Spotters.Data.UnitOfWork;
using Spotters.Service.Interface;
using Spotters.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Spotters.Service.Implementation
{
    public abstract class BaseService<TObject, TEntity> : IBaseService<TObject>
        where TObject : class, IBaseViewModel
        where TEntity : class, IBaseEntity
    {

        protected IMapper Mapper { get; set; }
        protected IUnitOfWork UOW { get; set; }
        protected IBaseRepository<TEntity> Repo { get; set; }

        public BaseService(IMapper mapper, IUnitOfWork uow, IBaseRepository<TEntity> repo)
        {
            this.Mapper = mapper;
            this.Repo = repo;
            this.UOW = uow;
        }

        public virtual async Task<TObject> AddAsync(TObject obj)
        {
            var entity = Mapper.Map<TEntity>(obj);
            var result = await Repo.CreateAsync(entity).ConfigureAwait(true);
            await UOW.Commit().ConfigureAwait(true);
            return Mapper.Map<TObject>(result);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var curObj = await Repo.FindByIdAsync(id).ConfigureAwait(true);
            if (curObj != null)
            {
                await Repo.DeleteAsync(curObj).ConfigureAwait(true);
                await UOW.Commit().ConfigureAwait(true);
            }
        }

        public virtual IEnumerable<TObject> GetAllAsync()
        {
            return Mapper.Map<IEnumerable<TObject>>(Repo.GetAll());
        }

        public async Task<TObject> GetEntityById(Guid id)
        {
            return Mapper.Map<TObject>(await Repo.FindByIdAsync(id).ConfigureAwait(true));
        }

        public virtual TObject GetById(Guid id)
        {
            return Mapper.Map<TObject>(Repo.GetByCondition(x => x.InternalId == id));
        }

        public virtual async Task<TObject> UpdateAsync(Guid id, TObject obj)
        {
            var curObj = await Repo.FindByIdAsync(id).ConfigureAwait(true);
            if (curObj == null)
            {
                return null;
            }
            var result = await Repo.UpdateAsync(Mapper.Map(obj, curObj)).ConfigureAwait(true);
            await UOW.Commit().ConfigureAwait(true);
            return Mapper.Map<TObject>(result);
        }
    }
}
