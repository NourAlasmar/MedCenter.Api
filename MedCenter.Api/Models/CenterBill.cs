using System;

namespace MedCenter.Api.Models
{
    /// <summary>فواتير تخص المركز نفسه (ثابتة/أقساط) لا تدخل بالحساب الربحي النهائي إذا كانت تطويرية.</summary>
    public class CenterBill : BaseEntity
    {
        public long CenterId { get; set; }
        public byte BillType { get; set; } // 1 Recurring, 2 OneTime/Installments
        public string Vendor { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public bool IsExcludedFromProfit { get; set; } = true;
    }
}