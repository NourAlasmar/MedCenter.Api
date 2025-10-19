namespace MedCenter.Api.Models
{
    public class SmsTemplate : BaseEntity
    {
        public long? CenterId { get; set; } // NULL = قالب عام
        public string Key { get; set; } = string.Empty; // Reminder/Cancel/Rx/Custom
        public string Body { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}