using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Spotters.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
