using Fabric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricModel
{
    public class Booking
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int StuffId { get; set; }
        public int? ExecuterId { get; set; }
        public int Count { get; set; }
        public decimal Cost { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime? DateBuilt { get; set; }
		public virtual Customer Customer { get; set; }
		public virtual Stuff Stuff { get; set; }
		public virtual Executer Executer { get; set; }
	}
}
