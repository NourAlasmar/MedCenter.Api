using System;

namespace MedCenter.Api.Models
{
    public class ActivityLog : BaseEntity
    {
        public long? CenterId { get; set; }
        public Guid? UserId { get; set; }
        public string Action { get; set; } = string.Empty; // CREATE/UPDATE/DELETE/LOGIN...
        public string? TargetTable { get; set; }
        public long? TargetId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? IPAddress { get; set; }
        public string? MetaJson { get; set; }
    }
}