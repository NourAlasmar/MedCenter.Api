using System;

namespace MedCenter.Api.Models
{
    /// <summary>موافقات موقعة من المريض (نماذج موافقة).</summary>
    public class PatientConsent : BaseEntity
    {
        public long PatientId { get; set; }
        public long CenterId { get; set; }
        public string ConsentType { get; set; } = string.Empty; // نوع الموافقة
        public DateTime SignedAt { get; set; }
        public string? DocumentUrl { get; set; }
    }
}