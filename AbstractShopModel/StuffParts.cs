namespace AbstractShopModel
{
    /// <summary>
    /// Сколько компонентов, требуется при изготовлении изделия
    /// </summary>
    public class StuffParts
    {
        public int Id { get; set; }

        public int StuffId { get; set; }

        public int PartId { get; set; }

        public int Amount { get; set; }
    }
}
