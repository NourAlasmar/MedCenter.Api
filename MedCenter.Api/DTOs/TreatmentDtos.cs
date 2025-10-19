// DTO بسيط لإضافة علاج جديد سواء للأسنان (ToothCode) أو الجلدية (AreaCode)

using MedCenter.Api.Models;

namespace MedCenter.Api.DTOs
{
    public record TreatmentCreateDto(
        long CenterId,            // المركز
        long PatientId,           // المريض
        Guid DoctorId,            // الطبيب المنفّذ
        long ProcedureId,         // الإجراء من الدليل
        string? ToothCode,        // إن كان علاج أسنان (مثال UR6/11/..)
        string? AreaCode,         // إن كان علاج جلد/تجميل (مثال Face.LeftCheek)
        string? Notes,            // ملاحظة اختيارية
        DateTime? ExecutedAtUtc,  // وقت التنفيذ (اختياري، افتراضي الآن)
        decimal? Price            // سعر الإجراء (اختياري — قد يُحتسب من التسعير)
    );
}