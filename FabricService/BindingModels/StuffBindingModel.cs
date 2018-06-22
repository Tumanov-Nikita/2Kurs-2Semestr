using System.Collections.Generic;

namespace FabricService.BindingModels
{
    public class StuffBindingModel
    {
        public int Id { get; set; }

        public string StuffName { get; set; }

        public decimal Cost { get; set; }

        public List<StuffPartsBindingModel> StuffParts { get; set; }
    }
}
