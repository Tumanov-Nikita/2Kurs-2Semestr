using FabricModel;
using System;
using System.Data.Entity;

namespace FabricService
{
    public class AlexeysDbContext : DbContext
    {
        public AlexeysDbContext() : base("FabricDatabase")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Part> Parts { get; set; }

        public virtual DbSet<Executer> Executers { get; set; }

        public virtual DbSet<Booking> Bookings { get; set; }

        public virtual DbSet<Stuff> Stuffs { get; set; }

        public virtual DbSet<StuffPart> StuffParts { get; set; }

        public virtual DbSet<Storage> Storages { get; set; }

        public virtual DbSet<StoragePart> StorageParts { get; set; }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception)
            {
                foreach (var entry in ChangeTracker.Entries())
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.Reload();
                            break;
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                    }
                }
                throw;
            }
        }
    }
}
