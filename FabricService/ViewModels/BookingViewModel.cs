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

        public int Amount { get; set; }

        public decimal Sum { get; set; }

        public string Status { get; set; }

        public string DateCreate { get; set; }

        public string DateExecute { get; set; }
    }
}
