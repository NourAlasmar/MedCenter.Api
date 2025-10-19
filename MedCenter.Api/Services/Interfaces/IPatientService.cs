
//  واجهة خدمة إدارة المرضى (IPatientService)
//
// الغرض:
// تحديد جميع العمليات الأساسية التي تتعلق بإدارة بيانات المرضى داخل النظام.
// تُستخدم هذه الواجهة كعقد (Contract) لضمان أن أي خدمة تُنفّذ منطق المرضى
// (مثل PatientService) تلتزم بنفس العمليات المعيارية.
//
// الفوائد:
//  تسهّل الاختبار (Unit Testing) والعزل بين الطبقات.
//  تُبقي الكود منظمًا وقابلًا للتوسعة.
//  تتيح استبدال أو تطوير منطق التعامل مع المرضى دون كسر واجهة الاستخدام.
//
// العمليات:
//
// 🔹 CreateAsync : إنشاء مريض جديد
//     - تُستخدم عند تسجيل مريض لأول مرة في النظام.
//     - تستقبل DTO يحتوي على بيانات المريض الأساسية (الاسم، الهاتف، رقم المركز).
//     - تُعيد كائن المريض الجديد بعد حفظه في قاعدة البيانات.
//
// 🔹 GetAsync : جلب بيانات مريض محدد
//     - تُعيد كائن المريض بناءً على المعرف (Id) أو null إن لم يكن موجودًا.
//
// 🔹 ListByCenterAsync : عرض قائمة المرضى لمركز طبي
//     - تُعيد جميع المرضى المرتبطين بمركز معين.
//     - تدعم البحث (q) بالاسم أو رقم الهاتف.
//     - تُفيد في واجهة الاستقبال أو صفحة الطبيب.
//
// 🔹 UpdateAsync : تحديث بيانات المريض
//     - تُحدّث بيانات المريض (الاسم، الجنس، تاريخ الميلاد، الملاحظات...).
//     - تُعيد true عند نجاح العملية، false إذا لم يتم العثور على المريض.


using MedCenter.Api.DTOs;
using MedCenter.Api.Models;

namespace MedCenter.Api.Services.Interfaces
{
    public interface IPatientService
    {
        Task<Patient> CreateAsync(PatientCreateDto dto, CancellationToken ct = default);
        Task<Patient?> GetAsync(long id, CancellationToken ct = default);
        Task<IEnumerable<Patient>> ListByCenterAsync(long centerId, string? q = null, CancellationToken ct = default);
        Task<bool> UpdateAsync(long id, PatientUpdateDto dto, CancellationToken ct = default);
    }
}