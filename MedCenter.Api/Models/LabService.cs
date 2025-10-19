namespace MedCenter.Api.Models
{
    /// <summary>قائمة خدمات/أعمال المخبر مع أسعار افتراضية.</summary>
    public class LabService : BaseEntity
    {
        public long CenterId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal? DefaultPrice { get; set; }
    }
}