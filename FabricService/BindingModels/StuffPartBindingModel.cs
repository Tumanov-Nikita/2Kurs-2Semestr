using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FabricService.BindingModels
{
    [DataContract]
    public class StuffPartBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int StuffId { get; set; }
        [DataMember]
        public int PartId { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}
