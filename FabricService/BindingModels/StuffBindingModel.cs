using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricService.BindingModels
{
	[DataContract]
	public class StuffBindingModel
    {
		[DataMember]
		public int Id { get; set; }
		[DataMember]
		public string StuffName { get; set; }
		[DataMember]
		public decimal Cost { get; set; }
		[DataMember]
		public List<StuffPartBindingModel> StuffParts { get; set; }

    }
}
