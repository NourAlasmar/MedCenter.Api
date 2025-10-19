using System;

namespace MedCenter.Api.Models
{
    public class Payment : BaseEntity
    {
        public long InvoiceId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; } = PaymentMethod.Cash;
        public string? TxnRef { get; set; }
        public string? Notes { get; set; }
    }
}