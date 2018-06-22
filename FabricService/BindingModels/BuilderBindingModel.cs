using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FabricService.BindingModels
{
    [DataContract]
    public class BuilderBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string BuilderFIO { get; set; }
    }
}

