using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricService.ViewModels
{
    public class StuffViewModel
    {
        public int Id { get; set; }
        public string StuffName { get; set; }
        public decimal Cost { get; set; }
        public List<StuffPartViewModel> StuffParts { get; set; }
    }
}
