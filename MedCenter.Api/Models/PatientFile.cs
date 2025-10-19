namespace MedCenter.Api.Models
{
    /// <summary>ملفات/صور تابعة للمريض (تُخزن كروابط).</summary>
    public class PatientFile : BaseEntity
    {
        public long PatientId { get; set; }
        public long CenterId { get; set; }
        public string? Folder { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string StorageUrl { get; set; } = string.Empty;
        public string? FileType { get; set; } // صورة، PDF، أشعة...
        public long? UploadedBy { get; set; }
    }
}