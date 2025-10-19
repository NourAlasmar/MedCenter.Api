using System;

namespace MedCenter.Api.Models
{
    /// <summary>تذكير يُرسل بالموعد (SMS/Email/InApp).</summary>
    public class AppointmentReminder : BaseEntity
    {
        public long AppointmentId { get; set; }
        public NotifyChannel Channel { get; set; } = NotifyChannel.Sms;
        public DateTime ScheduledAt { get; set; }
        public DateTime? SentAt { get; set; }
        public SendStatus Status { get; set; } = SendStatus.Pending;
    }
}