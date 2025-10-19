using System;

namespace MedCenter.Api.Models
{
    public class InventoryBatch : BaseEntity
    {
        public long ItemId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
    }
}