using System;

namespace MedCenter.Api.Models
{
    public class Appointment : BaseEntity
    {
        public long CenterId { get; set; }
        public Guid DoctorId { get; set; }
        public long? PatientId { get; set; }  // يمكن إضافة موعد بدون اختيار مريض مبدئيًا
        public DateTime AppointmentDate { get; set; }
        public int DurationMinutes { get; set; }
        public AppointmentType Type { get; set; } = AppointmentType.Consultation;
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Confirmed;
        public string? Notes { get; set; }
    }
}