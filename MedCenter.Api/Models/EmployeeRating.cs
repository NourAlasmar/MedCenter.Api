using System;

namespace MedCenter.Api.Models
{
    public class EmployeeRating : BaseEntity
    {
        public long EmployeeId { get; set; }
        public DateTime RatedAt { get; set; }
        public RatingScore Score { get; set; } = RatingScore.Three;
        public string? Notes { get; set; }
    }
}