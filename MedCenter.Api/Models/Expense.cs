using System;

namespace MedCenter.Api.Models
{
    public class Expense : BaseEntity
    {
        public long CenterId { get; set; }
        public string ExpenseName { get; set; } = string.Empty;
        public ExpenseCategory Category { get; set; } = ExpenseCategory.Other;

        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }

        public DateTime? DueDate { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }

        public PaymentStatus Status { get; set; } = PaymentStatus.Unpaid;
        public string? Notes { get; set; }
    }
}