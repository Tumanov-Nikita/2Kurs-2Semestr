namespace FabricService.BindingModels
{
    public class BookingBindingModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int StuffId { get; set; }

        public int? ExecuterId { get; set; }

        public int Amount { get; set; }

        public decimal Sum { get; set; }
    }
}
