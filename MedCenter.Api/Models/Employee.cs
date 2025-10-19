using System;

namespace MedCenter.Api.Models
{
    public class Employee : BaseEntity
    {
        public long CenterId { get; set; }
        public Guid? UserId { get; set; } // إن كان موظفًا ومستخدم نظام
        public string FullName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public byte SalaryType { get; set; } // 1 راتب، 2 نسبة ربح
        public DateTime HireDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }
    }
}