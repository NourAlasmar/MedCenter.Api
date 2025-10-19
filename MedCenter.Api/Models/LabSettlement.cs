using System;

namespace MedCenter.Api.Models
{
    /// <summary>تسوية مستحقات المخبر لفترة زمنية.</summary>
    public class LabSettlement : BaseEntity
    {
        public long CenterId { get; set; }
        public long LabId { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public decimal TotalDue { get; set; }
        public decimal Paid { get; set; }
        public DateTime? SettledAt { get; set; }
    }
}