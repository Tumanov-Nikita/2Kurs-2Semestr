using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricService.BindingModels
{
    public class BookingBindingModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int StuffId { get; set; }
        public int? ExecuterId { get; set; }
        public int Count { get; set; }
        public decimal Cost { get; set; }

    }
}
