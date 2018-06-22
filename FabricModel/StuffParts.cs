namespace FabricModel
{
    /// <summary>
    /// Сколько компонентов, требуется при изготовлении изделия
    /// </summary>
    public class StuffParts
    {
        public int Id { get; set; }

        public int StuffId { get; set; }

        public int PartId { get; set; }

        public int Count { get; set; }
    }
}
