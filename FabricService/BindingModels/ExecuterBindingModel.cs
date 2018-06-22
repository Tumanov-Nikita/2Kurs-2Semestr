using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FabricService.BindingModels
{
    [DataContract]
    public class ExecuterBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string ExecuterFIO { get; set; }
    }
}

