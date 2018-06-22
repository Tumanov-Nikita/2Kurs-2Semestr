using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FabricModel
{
    public class Executer
    {
        public int Id { get; set; }

		[Required]
		public string ExecuterFIO { get; set; }

		[ForeignKey("ExecuterId")]
		public virtual List<Booking> Bookings { get; set; }
	}
}
