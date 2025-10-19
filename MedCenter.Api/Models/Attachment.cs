namespace MedCenter.Api.Models
{
    /// <summary>مرفقات عامة (ملفات متنوعة).</summary>
    public class Attachment : BaseEntity
    {
        public long? CenterId { get; set; }
        public System.Guid? OwnerUserId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string? Mime { get; set; }
    }
}