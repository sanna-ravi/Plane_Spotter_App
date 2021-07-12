using Spotters.Data.Models.DBContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Spotters.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private SpottersDBContext SpottersDBContext { get; set; }

        public UnitOfWork(SpottersDBContext spottersDBContext)
        {
            this.SpottersDBContext = spottersDBContext;
        }

        public async Task Commit()
        {
            try
            {
                await SpottersDBContext.SaveChangesAsync().ConfigureAwait(true);
            }
            catch
            {

            }
        }
    }
}
