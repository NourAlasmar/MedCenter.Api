namespace MedCenter.Api.Models
{
    public class InventoryItem : BaseEntity
    {
        public long CenterId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? SKU { get; set; }
        public string Unit { get; set; } = "pcs";
        public int ReorderLevel { get; set; } = 0;
    }
}