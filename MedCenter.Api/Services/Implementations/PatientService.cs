// خدمة إدارة المرضى (PatientService)
//
// الغرض:
// تنفيذ المنطق الخاص بإدارة بيانات المرضى داخل النظام (العيادات والمراكز الطبية).
// تتولى هذه الخدمة التواصل مع قاعدة البيانات عبر وحدة العمل (IUnitOfWork)
// وتتعامل فقط مع الكيانات المرتبطة بالمريض (Patient).
//
// العمليات الأساسية:
//
//  CreateAsync : إنشاء مريض جديد
//     - تُستخدم عند تسجيل مريض لأول مرة في مركز طبي.
//     - تستقبل بيانات بسيطة (الاسم، الهاتف، رقم المركز).
//     - تحفظ المريض في قاعدة البيانات وتُعيد الكائن بعد إنشائه.
//
//  GetAsync : جلب بيانات مريض محدد
//     - تبحث عن المريض بالـ Id.
//     - تُعيد كائن المريض أو null إن لم يكن موجودًا.
//
//  ListByCenterAsync : عرض قائمة المرضى لمركز طبي
//     - تُعيد جميع المرضى التابعين لمركز معين.
//     - يمكن تمرير نص بحث (q) للبحث بالاسم أو رقم الهاتف.
//     - تُستخدم عادة في واجهات الاستقبال أو لوحة الطبيب.
//
//  UpdateAsync : تحديث بيانات المريض
//     - تُحدث المعلومات (الاسم، الهاتف، الجنس، تاريخ الميلاد، الملاحظات).
//     - تُسجّل وقت آخر تعديل (UpdatedAt).
//     - تُستخدم من قِبل الطبيب أو الاستقبال عند تحديث السجل.



using MedCenter.Api.DTOs;
using MedCenter.Api.Models;
using MedCenter.Api.Repositories.Interfaces;
using MedCenter.Api.Services.Interfaces;

namespace MedCenter.Api.Services.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _uow;
        public PatientService(IUnitOfWork uow) => _uow = uow;

        public async Task<Patient> CreateAsync(PatientCreateDto dto, CancellationToken ct = default)
        {
            var patient = new Patient { CenterId = dto.CenterId, FullName = dto.FullName, Phone = dto.Phone };
            await _uow.Patients.AddAsync(patient, ct);
            await _uow.SaveAsync(ct);
            return patient;
        }

        public Task<Patient?> GetAsync(long id, CancellationToken ct = default) => _uow.Patients.GetByIdAsync(id, ct);

        public Task<IEnumerable<Patient>> ListByCenterAsync(long centerId, string? q = null, CancellationToken ct = default)
            => _uow.Patients.GetAsync(p => p.CenterId == centerId && (q == null || p.FullName.Contains(q) || (p.Phone ?? "").Contains(q)), ct: ct);

        public async Task<bool> UpdateAsync(long id, PatientUpdateDto dto, CancellationToken ct = default)
        {
            var p = await _uow.Patients.GetByIdAsync(id, ct);
            if (p is null) return false;

            p.FullName = dto.FullName;
            p.Phone = dto.Phone;
            p.Gender = dto.Gender;
            p.BirthDate = dto.BirthDate;
            p.Notes = dto.Notes;
            p.UpdatedAt = DateTime.UtcNow;

            _uow.Patients.Update(p);
            await _uow.SaveAsync(ct);
            return true;
        }
    }
}