namespace MedCenter.Api.Models
{
    /// <summary>بدلات/خصومات/مكافآت شهرية.</summary>
    public class PayrollAdjustment : BaseEntity
    {
        public long EmployeeId { get; set; }
        public byte Month { get; set; }
        public short Year { get; set; }
        public byte Type { get; set; } // 1 بدل، 2 خصم، 3 مكافأة
        public decimal Amount { get; set; }
        public string? Reason { get; set; }
    }
}