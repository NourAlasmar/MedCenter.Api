//  واجهة خدمة المواعيد (IAppointmentService)
//
// الغرض:
// تعريف العمليات الأساسية التي يجب أن تنفذها أي خدمة تتعامل مع المواعيد الطبية (Appointments).
// هذه الواجهة تُستخدم لعزل الطبقات (Decoupling) بين منطق الأعمال (Business Logic)
// وتنفيذ الخدمة الفعلي (AppointmentService).
//
// الفوائد:
//  تسهّل الاختبار (Unit Testing) عبر الاعتماد على واجهة بدلاً من التنفيذ المباشر.
//  تُبقي الكود مرنًا وقابلًا للتوسّع مستقبلاً.
// تتيح تعدد التطبيقات (Implementations) إن تغيرت آلية حفظ أو إدارة المواعيد.
//
// العمليات المعرّفة:
//
//  CreateAsync : إنشاء موعد جديد
//     - يُنشئ موعدًا بين الطبيب والمريض بناءً على المعلومات الواردة من واجهة الاستخدام.
//
//  ForDoctorAsync : جلب مواعيد طبيب
//     - تُعيد قائمة المواعيد الخاصة بطبيب محدد ضمن فترة زمنية معينة (من - إلى).
//     - تُستخدم في واجهة التقويم (Calendar) للطبيب.
//
//  CancelAsync : إلغاء موعد
//     - تُحدث حالة الموعد إلى "ملغى" وتُخزن سبب الإلغاء (إن وُجد).
//     - تُستخدم في حال إلغاء المريض أو الطبيب للموعد.
//
//  CompleteAsync : إكمال موعد
//     - تُغير حالة الموعد إلى "مكتمل" عند الانتهاء من الجلسة الطبية.
//     - تُستخدم عادة في نهاية الزيارة لربطها بسجلات العلاج أو الفواتير.


using MedCenter.Api.DTOs;
using MedCenter.Api.Models;

namespace MedCenter.Api.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<Appointment> CreateAsync(AppointmentCreateDto dto, CancellationToken ct = default);
        Task<IEnumerable<Appointment>> ForDoctorAsync(Guid doctorId, DateTime from, DateTime to, CancellationToken ct = default);
        Task<bool> CancelAsync(long id, string? reason = null, CancellationToken ct = default);
        Task<bool> CompleteAsync(long id, CancellationToken ct = default);
    }
}