using System;

namespace MedCenter.Api.Models
{
    public class Attendance : BaseEntity
    {
        public long EmployeeId { get; set; }
        public long CenterId { get; set; }
        public DateTime Day { get; set; }
        public TimeSpan? CheckIn { get; set; }
        public TimeSpan? CheckOut { get; set; }
        public int OvertimeMinutes { get; set; }
        public string? Notes { get; set; }
    }
}