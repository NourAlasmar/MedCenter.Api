using System;

namespace MedCenter.Api.Models
{
    /// <summary>تصدير كشف مالي (PDF/Excel) للفترة.</summary>
    public class FinancialStatement : BaseEntity
    {
        public long CenterId { get; set; }
        public Guid? DoctorId { get; set; } // بيان خاص بطبيب
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public string? Url { get; set; }
        public DateTime GeneratedAt { get; set; }
    }
}