using System;

namespace MedCenter.Api.Models
{
    public class CenterBillInstallment : BaseEntity
    {
        public long CenterBillId { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PaidAt { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Unpaid;
    }
}