using System.ComponentModel.DataAnnotations;

namespace MedCenter.Api.Models
{
    // جدول يمثّل "سنًّا" واحدًا على مخطط مريض معيّن، مع حالة العرض البصري
    public class PatientTooth : BaseEntity
    {
        public long PatientId { get; set; }     // ربط بالمريض
        [MaxLength(10)]
        public string ToothCode { get; set; } = default!; // كود السن (FDI مثل 11..48 أو UR6/LL4)
        public ToothStatus Status { get; set; } = ToothStatus.Healthy;

        // JSON حر لحالة الواجهة: تلوين الأسطح، أيقونات، ملاحظات سريعة…
        // مثال محتوى:
        // { "surfaces": { "O":"planned", "M":"completed" }, "color":"#A0A0A0", "icons":["crown"] }
        public string? VisualStateJson { get; set; }

        [MaxLength(400)]
        public string? Notes { get; set; }      // ملاحظة للسن (اختياري)

        public long CenterId { get; set; }      // تبعية للمركز (لتقسيم البيانات)
        public Guid? DoctorId { get; set; }     // آخر من عدّل (للتتبع)
    }
}