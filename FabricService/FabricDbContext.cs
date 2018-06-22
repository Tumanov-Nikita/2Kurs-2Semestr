using FabricModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace FabricService
{
	[Table("FabricDatabase")]
	public class FabricDbContext : DbContext
	{
		public FabricDbContext()
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
	}
}
