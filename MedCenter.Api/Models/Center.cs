using System.Collections.Generic;

namespace MedCenter.Api.Models
{
    /// <summary>المركز الطبي (Tenant).</summary>
    public class Center : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public SubscriptionType SubscriptionType { get; set; } = SubscriptionType.Monthly;
        public System.DateTime SubscriptionStartDate { get; set; }
        public System.DateTime SubscriptionEndDate { get; set; }

        // هوية بصرية
        public string? LogoUrl { get; set; }
        public string? InvoiceTemplateUrl { get; set; }
        public string? BrandFrameJson { get; set; }

        public bool IsActive { get; set; } = true;

        // تنقض لاحقًا عبر Config
        public ICollection<CenterUser> Users { get; set; } = new List<CenterUser>();
    }
}