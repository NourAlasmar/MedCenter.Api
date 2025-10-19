namespace MedCenter.Api.Models
{
    public class Specialty : BaseEntity
    {
        public string Name { get; set; } = string.Empty; // أسنان، جلدية، تجميل، عيون...
        public bool IsActive { get; set; } = true;
    }
}