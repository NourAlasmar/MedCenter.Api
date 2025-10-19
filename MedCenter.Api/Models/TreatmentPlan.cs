using System;

namespace MedCenter.Api.Models
{
    /// <summary>خطة العلاج العامة للمريض.</summary>
    public class TreatmentPlan : BaseEntity
    {
        public long PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public long CenterId { get; set; }
        public PlanStatus Status { get; set; } = PlanStatus.Draft;
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}