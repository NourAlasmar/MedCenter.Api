using System;

namespace MedCenter.Api.Models
{
    /// <summary>وصفة طبية (يمكن إرسالها SMS).</summary>
    public class Prescription : BaseEntity
    {
        public long CenterId { get; set; }
        public long PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime IssuedAt { get; set; }
        public string Body { get; set; } = string.Empty; // نص الوصفة
        public bool SentBySms { get; set; } = false;
        public long? SmsMessageId { get; set; } // FK -> SmsOutbox
    }
}