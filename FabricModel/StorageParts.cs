namespace FabricModel
{
    /// <summary>
    /// Сколько компонентов хранится на складе
    /// </summary>
    public class StorageParts
    {
        public int Id { get; set; }

        public int StorageId { get; set; }

        public int PartId { get; set; }

        public int Count { get; set; }
    }
}
