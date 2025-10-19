namespace MedCenter.Api.Models
{
    public class Lab : BaseEntity
    {
        public long CenterId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Location { get; set; }
        public string? Phone { get; set; }
    }
}