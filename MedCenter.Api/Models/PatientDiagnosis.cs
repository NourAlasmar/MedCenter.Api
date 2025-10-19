using System;

namespace MedCenter.Api.Models
{
    /// <summary>تشخيص مسجل للمريض (مرتبط بقاموس التشخيصات).</summary>
    public class PatientDiagnosis : BaseEntity
    {
        public long PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public long CenterId { get; set; }
        public long DiagnosisId { get; set; } // FK -> DiagnosisCatalog
        public string? Notes { get; set; }
        public DateTime RecordedAt { get; set; }
    }
}