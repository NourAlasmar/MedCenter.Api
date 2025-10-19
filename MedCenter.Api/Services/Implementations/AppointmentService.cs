// خدمة المواعيد AppointmentService:
// - توفّر عمليات الأعمال (Business Logic) الخاصة بالمواعيد بعيدًا عن طبقة الـ Controllers.
// - تعتمد على وحدة العمل IUnitOfWork للوصول إلى المستودعات (Repositories) وحفظ التغييرات في معاملة واحدة.
// - ما تقوم به الخدمة حالياً:
//   * CreateAsync : إنشاء موعد جديد بناءً على DTO قادم من الـ API.
//   * ForDoctorAsync : جلب مواعيد طبيب ضمن فترة زمنية مرتبة حسب التاريخ.
//   * CancelAsync : إلغاء موعد مع إمكانية تدوين سبب الإلغاء ضمن الملاحظات.
//   * CompleteAsync : تعليم الموعد كمكتمل.


using MedCenter.Api.DTOs;
using MedCenter.Api.Models;
using MedCenter.Api.Repositories.Interfaces;
using MedCenter.Api.Services.Interfaces;

namespace MedCenter.Api.Services.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _uow; // وحدة العمل لتجميع عمليات الحفظ عبر عدة مستودعات

        public AppointmentService(IUnitOfWork uow) => _uow = uow;

        // إنشاء موعد جديد:
        // - يبني كيان Appointment من الـ DTO.
        // - يحفظه عبر المستودع ثم يستدعي SaveAsync لحفظ المعاملة.
        public async Task<Appointment> CreateAsync(AppointmentCreateDto dto, CancellationToken ct = default)
        {
            var a = new Appointment
            {
                CenterId = dto.CenterId,
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                AppointmentDate = dto.AppointmentDate,
                DurationMinutes = dto.DurationMinutes,
                Type = dto.Type,
                Notes = dto.Notes
            };


            await _uow.Appointments.AddAsync(a, ct);
            await _uow.SaveAsync(ct);
            return a;
        }

        // جلب مواعيد طبيب ضمن فترة [from..to] مرتبة تصاعديًا حسب التاريخ:
        // - تستخدم مستودع المواعيد مع شرط التصفية والترتيب.
        public Task<IEnumerable<Appointment>> ForDoctorAsync(Guid doctorId, DateTime from, DateTime to, CancellationToken ct = default)
            => _uow.Appointments.GetAsync(
                   a => a.DoctorId == doctorId && a.AppointmentDate >= from && a.AppointmentDate <= to,
                   orderBy: q => q.OrderBy(x => x.AppointmentDate),
                   ct: ct);

        // إلغاء موعد:
        // - يجلب الموعد، وإن لم يوجد يُعيد false.
        // - يغيّر الحالة إلى Cancelled ويُحدّث UpdatedAt.
        // - يضيف سبب الإلغاء إلى الملاحظات إن تم تمريره.
        // - يحفظ التغييرات.
        public async Task<bool> CancelAsync(long id, string? reason = null, CancellationToken ct = default)
        {
            var a = await _uow.Appointments.GetByIdAsync(id, ct);
            if (a is null) return false;

            a.Status = AppointmentStatus.Cancelled;
            a.UpdatedAt = DateTime.UtcNow;

            a.Notes = string.IsNullOrWhiteSpace(reason)
                ? a.Notes
                : $"{a.Notes}\n[إلغاء]: {reason}";

            _uow.Appointments.Update(a);
            await _uow.SaveAsync(ct);

            return true;
        }

        // إكمال موعد:
        // - يجلب الموعد، وإن لم يوجد يُعيد false.
        // - يغيّر الحالة إلى Completed ويُحدّث UpdatedAt ثم يحفظ.
        public async Task<bool> CompleteAsync(long id, CancellationToken ct = default)
        {
            var a = await _uow.Appointments.GetByIdAsync(id, ct);
            if (a is null) return false;

            a.Status = AppointmentStatus.Completed;
            a.UpdatedAt = DateTime.UtcNow;

            _uow.Appointments.Update(a);
            await _uow.SaveAsync(ct);

            return true;
        }
    }
}