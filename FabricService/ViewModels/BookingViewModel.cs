using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricService.ViewModels
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFIO { get; set; }
        public int StuffId { get; set; }
        public string StuffName { get; set; }
        public int? ExecuterId { get; set; }
        public string ExecuterName { get; set; }
        public int Count { get; set; }
        public decimal Cost { get; set; }
        public string Status { get; set; }
        public string DateBegin { get; set; }
        public string DateBuilt { get; set; }

    }
}
