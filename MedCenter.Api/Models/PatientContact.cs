namespace MedCenter.Api.Models
{
    /// <summary>شخص تواصل/طوارئ للمريض.</summary>
    public class PatientContact : BaseEntity
    {
        public long PatientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Relation { get; set; }
        public string? Phone { get; set; }
    }
}