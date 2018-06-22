using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricService.BindingModels
{
    public class StuffPartBindingModel
    {
        public int Id { get; set; }
        public int StuffId { get; set; }
        public int PartId { get; set; }
        public int Count { get; set; }
    }
}
