namespace MedCenter.Api.Models
{
    public class ProcedureCatalog : BaseEntity
    {
        public short SpecialtyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public decimal? DefaultCost { get; set; }
        public bool IsActive { get; set; } = true;
    }
}