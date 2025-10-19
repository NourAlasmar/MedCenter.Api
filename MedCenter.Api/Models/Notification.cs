using System;

namespace MedCenter.Api.Models
{
    public class Notification : BaseEntity
    {
        public long? CenterId { get; set; } // إشعار عام أو لمركز محدد
        public Guid? UserId { get; set; }   // إشعار لمستخدم
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public NotifyChannel Type { get; set; } = NotifyChannel.InApp;
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}