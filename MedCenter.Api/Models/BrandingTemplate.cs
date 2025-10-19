namespace MedCenter.Api.Models
{
    /// <summary>قوالب الطباعة حسب المركز (فاتورة/وصفة/تقرير...).</summary>
    public class BrandingTemplate : BaseEntity
    {
        public long CenterId { get; set; }
        public BrandingTemplateType Type { get; set; } = BrandingTemplateType.GeneralPrint;
        public string TemplateJson { get; set; } = string.Empty;
        public bool Active { get; set; } = true;
    }
}