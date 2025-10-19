namespace MedCenter.Api.Models
{
    /// <summary>بنود خطة العلاج (إجراءات مخططة/مؤكدة/منفذة).</summary>
    public class TreatmentPlanItem : BaseEntity
    {
        public long PlanId { get; set; }
        public long ProcedureId { get; set; } // FK -> ProcedureCatalog
        public string? ToothCode { get; set; } // لطب الأسنان
        public string? AreaCode { get; set; }  // للجلدية/التجميل
        public int Qty { get; set; } = 1;

        public decimal? AgreedPrice { get; set; } // يسمح يدويًا عند غياب تسعيرة المركز
        public decimal? Discount { get; set; }
        public string? Notes { get; set; }
        public bool IsConfirmed { get; set; } = false;
        public bool IsExecuted { get; set; } = false;
    }
}