using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricModel
{
    public class StuffPart
    {
        public int Id { get; set; }
        public int StuffId { get; set; }
        public int PartId { get; set; }
        public int Count { get; set; }
		public virtual Stuff Stuff { get; set; }
		public virtual Part Part { get; set; }
	}
}
