using System;

namespace MedCenter.Api.Models
{
    /// <summary>تسعير إجراء معين لمركز معيّن (يتجاوز الافتراضي). </summary>
    public class CenterProcedurePrice : BaseEntity
    {
        public long CenterId { get; set; }
        public long ProcedureId { get; set; }
        public decimal Price { get; set; }
        public string CurrencyCode { get; set; } = "USD";
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
    }
}