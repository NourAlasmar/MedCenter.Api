using System;

namespace MedCenter.Api.Models
{
    public class EmployeeContract : BaseEntity
    {
        public long EmployeeId { get; set; }
        public string ContractType { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public byte Status { get; set; } // ساري/منتهي/موقوف
        public string? DocumentUrl { get; set; }
    }
}