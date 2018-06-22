namespace FabricService.BindingModels
{
    public class BookingBindingModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int StuffId { get; set; }

        public int? BuilderId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }
    }
}
