using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Upormium.Model.Models;
using Upormium.Model.Models.Users;

namespace Upormium.Model.DbContext
{
    public class UpormiumDbContext : IdentityDbContext<User>
    {
        #region Constructors

        public UpormiumDbContext(){}

        public UpormiumDbContext(DbContextOptions<UpormiumDbContext> options): base(options)
        {

        }
        #endregion

        #region Protected Methods

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        #endregion

        #region Overriden Methods


        /// <summary>
        /// To override savechanges method
        /// </summary>
        public override int SaveChanges()
        {
            ChangeTracker.Entries().Where(x => x.Entity is BaseModel && x.State == EntityState.Added).ToList().ForEach(x =>
            {
                ((BaseModel)x.Entity).CreatedDateTime = DateTime.UtcNow;
            });
            ChangeTracker.Entries().Where(x => x.Entity is BaseModel && x.State == EntityState.Modified).ToList().ForEach(x =>
            {
                ((BaseModel)x.Entity).UpdatedDateTime = DateTime.UtcNow;
            });

            return base.SaveChanges();
        }
 

        /// <summary>
        /// To override savechangesasync method
        /// </summary>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.Entries().Where(x => x.Entity is BaseModel && x.State == EntityState.Added).ToList().ForEach(x =>
            {
                ((BaseModel)x.Entity).CreatedDateTime = DateTime.UtcNow;
            });
            ChangeTracker.Entries().Where(x => x.Entity is BaseModel && x.State == EntityState.Modified).ToList().ForEach(x =>
            {
                ((BaseModel)x.Entity).UpdatedDateTime = DateTime.UtcNow;
            });

            return base.SaveChangesAsync(cancellationToken);
        }
        #endregion
    }
}
