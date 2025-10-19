namespace MedCenter.Api.Models
{
    public class EmployeeDocument : BaseEntity
    {
        public long EmployeeId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string? Mime { get; set; }
    }
}