using System;

namespace MedCenter.Api.Models
{
    /// <summary>
    /// كيان أساسي يحتوي أعمدة التدقيق والحذف المنطقي.
    /// </summary>
    public abstract class BaseEntity
    {
        public long Id { get; set; }

        // تدقيق
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public long? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public long? UpdatedBy { get; set; }

        // حذف منطقي
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public long? DeletedBy { get; set; }
    }
}