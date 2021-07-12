using Spotters.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Spotters.Service.Interface
{
    public interface IBaseService<TObject>
        where TObject : class, IBaseViewModel
    {
        IEnumerable<TObject> GetAllAsync();
        
        TObject GetById(Guid id);

        Task<TObject> GetEntityById(Guid id);

        Task<TObject> AddAsync(TObject obj);

        Task<TObject> UpdateAsync(Guid id, TObject obj);

        Task DeleteAsync(Guid id);
    }
}
