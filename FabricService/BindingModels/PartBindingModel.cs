using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FabricService.BindingModels
{
    [DataContract]
    public class PartBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string PartName { get; set; }

    }
}
