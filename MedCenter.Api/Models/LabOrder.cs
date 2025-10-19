using System;

namespace MedCenter.Api.Models
{
    public class LabOrder : BaseEntity
    {
        public long CenterId { get; set; }
        public long LabId { get; set; }
        public Guid DoctorId { get; set; }
        public long PatientId { get; set; }
        public long ServiceId { get; set; } // FK -> LabService
        public string? ToothCode { get; set; } // لطب الأسنان
        public int Qty { get; set; } = 1;
        public decimal UnitPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public byte Status { get; set; } // 0 قيد التنفيذ، 1 جاهز، 2 مسلّم، 3 مُلغى
    }
}