namespace MedCenter.Api.Models
{
    public class DiagnosisCatalog : BaseEntity
    {
        public short SpecialtyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public long? CenterId { get; set; } // NULL = تشخيص عام لكل المراكز
        public bool IsActive { get; set; } = true;
    }
}