namespace MedCenter.Api.Models
{
    /// <summary>شكاوى/اقتراحات من المرضى أو المستخدمين.</summary>
    public class Feedback : BaseEntity
    {
        public long CenterId { get; set; }
        public long? PatientId { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Type { get; set; } = "Suggestion"; // Complaint/Suggestion
        public byte Status { get; set; } = 0; // 0 جديد، 1 قيد المعالجة، 2 مغلق
    }
}