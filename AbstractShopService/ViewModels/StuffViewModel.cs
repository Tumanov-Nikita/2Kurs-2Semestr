using System.Collections.Generic;

namespace AbstractShopService.ViewModels
{
    public class StuffViewModel
    {
        public int Id { get; set; }

        public string StuffName { get; set; }

        public decimal Cost { get; set; }

        public List<StuffPartsViewModel> StuffParts { get; set; }
    }
}
