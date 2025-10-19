using System;

namespace MedCenter.Api.Models
{
    /// <summary>إجراء منفّذ بالفعل (يظهر في صفحة العلاجات المنجزة والفواتير).</summary>
    public class Treatment : BaseEntity
    {
        public long CenterId { get; set; }
        public long PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public long ProcedureId { get; set; }
        public string? ToothCode { get; set; }
        public string? AreaCode { get; set; }
        public int Qty { get; set; } = 1;
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public DateTime ExecutedAt { get; set; }
        public string? Notes { get; set; }
    }
}