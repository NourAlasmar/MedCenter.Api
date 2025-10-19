using System;

namespace MedCenter.Api.Models
{
    public class DailyKpi : BaseEntity
    {
        public long CenterId { get; set; }
        public DateTime Day { get; set; } // فقط التاريخ
        public int PatientsCount { get; set; }
        public int NewPatients { get; set; }
        public int Appointments { get; set; }
        public decimal Revenue { get; set; }
        public decimal Expenses { get; set; }
        public decimal LabCost { get; set; }
        // NetIncome = Revenue - Expenses - LabCost (يمكن جعله View/Computed في DB)
    }
}