using System;

namespace MedCenter.Api.Models
{
    public class Invoice : BaseEntity
    {
        public long CenterId { get; set; }
        public long PatientId { get; set; }
        public Guid? DoctorId { get; set; }

        public string InvoiceNo { get; set; } = string.Empty;
        public string CurrencyCode { get; set; } = "USD";

        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;

        public DateTime InvoiceDate { get; set; }
        public string? Notes { get; set; }
    }
}