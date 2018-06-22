using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FabricService.ViewModels
{
    [DataContract]
    public class BookingViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int CustomerId { get; set; }
        [DataMember]
        public string CustomerFIO { get; set; }
        [DataMember]
        public int StuffId { get; set; }
        [DataMember]
        public string StuffName { get; set; }
        [DataMember]
        public int? ExecuterId { get; set; }
        [DataMember]
        public string ExecuterName { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public decimal Cost { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string DateBegin { get; set; }
        [DataMember]
        public string DateBuilt { get; set; }

    }
}
