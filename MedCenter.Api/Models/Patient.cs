using System;

namespace MedCenter.Api.Models
{
    /// <summary>بيانات المريض الأساسية.</summary>
    public class Patient : BaseEntity
    {
        public long CenterId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public byte? Gender { get; set; }  // 0 غير محدد، 1 ذكر، 2 أنثى
        public DateTime? BirthDate { get; set; }
        public byte? ReferralSource { get; set; } // 1 إعلان، 2 توصية، 3 شركة
        public string? PreferredLanguage { get; set; } // ar/en
        public bool IsVIP { get; set; } = false;
        public string? Notes { get; set; }
    }
}