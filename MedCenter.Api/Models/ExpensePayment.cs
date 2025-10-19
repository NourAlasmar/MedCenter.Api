using System;

namespace MedCenter.Api.Models
{
    /// <summary>دفعات على مصروف معيّن.</summary>
    public class ExpensePayment : BaseEntity
    {
        public long ExpenseId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; } = PaymentMethod.Cash;
    }
}