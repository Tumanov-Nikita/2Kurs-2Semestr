using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using FabricModel;


[Table("FabricDatabase")]
public class FabricDbContext : DbContext
{
    public FabricDbContext()
    {
        Configuration.ProxyCreationEnabled = false;
        Configuration.LazyLoadingEnabled = false;
        var esureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Parts> Parts { get; set; }

    public virtual DbSet<Executer> Executers { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Stuff> Stuffs { get; set; }

    public virtual DbSet<StuffParts> StuffParts { get; set; }

    public virtual DbSet<Storage> Storages { get; set; }

    public virtual DbSet<StorageParts> StorageParts { get; set; }
}

