using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricService.BindingModels
{
    public class StuffBindingModel
    {
        public int Id { get; set; }
        public string StuffName { get; set; }
        public decimal Cost { get; set; }
        public List<StuffPartBindingModel> StuffParts { get; set; }

    }
}
