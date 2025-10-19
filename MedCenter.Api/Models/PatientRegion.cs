using System.ComponentModel.DataAnnotations;

namespace MedCenter.Api.Models
{
    // جدول يُمثّل منطقة جلدية/تجميلية محددة على مخطط المريض (مثال: Face.LeftCheek)
    public class PatientRegion : BaseEntity
    {
        public long PatientId { get; set; }
        [MaxLength(50)]
        public string RegionCode { get; set; } = default!; // مثال: Face.LeftCheek / Face.Forehead / Abdomen.Upper
        public RegionStatus Status { get; set; } = RegionStatus.Normal;

        // JSON حر لحالة العرض: لون المنطقة، أيقونة إجراء (مقص/حقنة/ليزر)، شدة/درجة…
        // مثال:
        // { "color":"#FFD54F", "icons":["botox"], "intensity": 0.7 }
        public string? VisualStateJson { get; set; }

        [MaxLength(400)]
        public string? Notes { get; set; }

        public long CenterId { get; set; }
        public Guid? DoctorId { get; set; }
    }
}