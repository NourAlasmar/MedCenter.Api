namespace MedCenter.Api.Models
{
    public class InvoiceItem : BaseEntity
    {
        public long InvoiceId { get; set; }
        public long? TreatmentId { get; set; } // عند توفّر الإجراء المنفذ
        public long? ProcedureId { get; set; } // عند عدم وجود Treatment بعد
        public string Description { get; set; } = string.Empty;
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? Discount { get; set; }
    }
}