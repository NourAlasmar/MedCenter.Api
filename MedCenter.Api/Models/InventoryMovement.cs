using System;

namespace MedCenter.Api.Models
{
    public class InventoryMovement : BaseEntity
    {
        public long CenterId { get; set; }
        public long ItemId { get; set; }
        public long? BatchId { get; set; }
        public InventoryMovementType MovementType { get; set; } = InventoryMovementType.Purchase;
        public int Qty { get; set; }
        public string? RefTable { get; set; }  // مرجع الحركة
        public long? RefId { get; set; }
        public DateTime MovedAt { get; set; }
    }
}