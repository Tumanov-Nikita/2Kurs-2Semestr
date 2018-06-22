namespace FabricService.BindingModels
{
    public class StoragePartsBindingModel
    {
        public int Id { get; set; }

        public int StorageId { get; set; }

        public int PartId { get; set; }

        public int Amount { get; set; }
    }
}
