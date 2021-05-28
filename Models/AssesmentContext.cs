using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AssessmentMVC.Models
{
    public partial class AssesmentContext : DbContext
    {
        public override int SaveChanges()
        {
            this.AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            this.AddTimestamps();
             return (await base.SaveChangesAsync());
        }

        // AssessmentMVC.Models.AssesmentContext
        private void AddTimestamps()
        {
            IEnumerable<DbEntityEntry> arg_2A_0 = base.ChangeTracker.Entries();
            
            string user = HttpContext.Current.User.Identity.Name ;
            foreach (DbEntityEntry current in arg_2A_0)
            {
                if (current.State == EntityState.Added)
                {
                    ((IBaseEntity)current.Entity).CreatedDate = DateTime.Now;
                    ((IBaseEntity)current.Entity).CreatedBy = user;
                }
                else if (current.State == EntityState.Modified)
                {
                    ((IBaseEntity)current.Entity).ModifiedDate = new DateTime?(DateTime.Now);
                    ((IBaseEntity)current.Entity).ModifiedBy = user;
                }
            }
        }

    }
}