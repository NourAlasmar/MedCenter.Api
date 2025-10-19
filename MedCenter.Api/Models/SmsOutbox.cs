using System;

namespace MedCenter.Api.Models
{
    /// <summary>طابور إرسال الرسائل القصيرة.</summary>
    public class SmsOutbox : BaseEntity
    {
        public long? CenterId { get; set; }
        public string ToPhone { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string? RelatedEntity { get; set; } // Appointment/Invoice/Prescription
        public long? RelatedId { get; set; }
        public DateTime? ScheduledAt { get; set; }
        public DateTime? SentAt { get; set; }
        public SendStatus Status { get; set; } = SendStatus.Pending;
        public string? ProviderMsgId { get; set; }
    }
}