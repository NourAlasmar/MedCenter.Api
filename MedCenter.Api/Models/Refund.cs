using System;

namespace MedCenter.Api.Models
{
    /// <summary>مرتجع/استرداد مالي.</summary>
    public class Refund : BaseEntity
    {
        public long CenterId { get; set; }
        public long? PatientId { get; set; }
        public Guid? DoctorId { get; set; }
        public long? InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public string? Reason { get; set; }
        public DateTime RefundDate { get; set; }
    }
}