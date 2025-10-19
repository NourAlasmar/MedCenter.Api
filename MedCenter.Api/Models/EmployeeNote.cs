namespace MedCenter.Api.Models
{
    public class EmployeeNote : BaseEntity
    {
        public long EmployeeId { get; set; }
        public string Note { get; set; } = string.Empty;
    }
}