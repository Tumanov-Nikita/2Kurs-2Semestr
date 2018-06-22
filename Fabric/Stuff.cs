using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FabricModel
{
    public class Stuff
    {
        public int Id { get; set; }

		[Required]
		public string StuffName { get; set; }

		[Required]
		public decimal Cost { get; set; }

		[ForeignKey("StuffId")]
		public virtual List<Booking> Booking { get; set; }

		[ForeignKey("StuffId")]
		public virtual List<StuffPart> StuffParts { get; set; }

	}
}
