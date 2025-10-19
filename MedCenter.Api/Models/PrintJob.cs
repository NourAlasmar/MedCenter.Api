using System;

namespace MedCenter.Api.Models
{
    public class PrintJob : BaseEntity
    {
        public long CenterId { get; set; }
        public string Entity { get; set; } = string.Empty;
        public long EntityId { get; set; }
        public DateTime PrintedAt { get; set; } = DateTime.UtcNow;
        public Guid? PrintedBy { get; set; } // UserId
    }
}