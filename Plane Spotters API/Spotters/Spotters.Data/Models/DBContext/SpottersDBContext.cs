using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Spotters.Core.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Spotters.Data.Models.DBContext
{
    public class SpottersDBContext : IdentityDbContext<ApplicationUser>
    {
        public SpottersDBContext(DbContextOptions<SpottersDBContext> options)
            : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var entry in this.ChangeTracker.Entries())
            {
                var now = DateTime.UtcNow;
                dynamic entity = entry.Entity;

                if (ObjectHandler.IsPropertyExist(entity, "UpdatedOn"))
                {
                    if (entry.State == EntityState.Added)
                    {
                        if (ObjectHandler.IsPropertyExist(entity, "CreatedOn"))
                        {
                            entity.CreatedOn = now;
                        }
                        if (ObjectHandler.IsPropertyExist(entity, "InternalId"))
                        {
                            entity.InternalId = Guid.NewGuid();
                        }
                        entity.UpdatedOn = now;
                    }
                    else
                    {
                        entity.UpdatedOn = now;
                    }
                }
            }
            this.ChangeTracker.DetectChanges();
            return base.SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.BuildIndexesFromAnnotations();
        }

        public DbSet<PlaneSpotter> PlaneSpotters { get; set; }
    }
}
