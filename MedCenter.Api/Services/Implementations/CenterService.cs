//  خدمة إدارة المراكز الطبية (CenterService)
//
// هذه الخدمة مسؤولة عن تنفيذ العمليات الأساسية الخاصة بالمراكز الطبية في النظام.
// تُعدّ واجهة بين طبقة الـ API (Controllers) وطبقة البيانات (Repositories عبر IUnitOfWork).
// تضم العمليات التالية:
//
// 🔹 CreateAsync : إنشاء مركز طبي جديد.
//     - يُنشئ كائن Center جديد ويحدد تواريخ الاشتراك (افتراضيًا شهر واحد).
//     - تُستخدم عادة عند اشتراك مركز جديد في النظام.
//
// 🔹 GetAsync : جلب بيانات مركز محدد عبر الـ Id.
//
// 🔹 SearchAsync : البحث عن المراكز باستخدام جزء من الاسم أو العنوان.
//     - تُفيد في صفحات الإدارة للعثور على المراكز بسهولة.
//
// 🔹 UpdateAsync : تحديث بيانات مركز موجود.
//     - لتعديل المعلومات الأساسية (الاسم، الهاتف، البريد...).
//     - يتضمّن أيضًا تفعيل أو تعطيل المركز عبر خاصية IsActive.
//
// 🔹 ExtendSubscriptionAsync : تمديد مدة الاشتراك لمركز معين.
//     - يُضيف مدة زمنية (TimeSpan) إلى تاريخ انتهاء الاشتراك الحالي.
//     - تُستخدم من قبل الأدمن عند تجديد الاشتراك أو من لوحة الفوترة.
//

using MedCenter.Api.DTOs;
using MedCenter.Api.Models;
using MedCenter.Api.Repositories.Interfaces;
using MedCenter.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedCenter.Api.Services.Implementations
{
    public class CenterService : ICenterService
    {
        private readonly IUnitOfWork _uow;

        public CenterService(IUnitOfWork uow) => _uow = uow;

        public async Task<Center> CreateAsync(CenterCreateDto dto, CancellationToken ct = default)
        {
            var entity = new Center
            {
                Name = dto.Name,
                Address = dto.Address,
                Phone = dto.Phone,
                Email = dto.Email,
                SubscriptionStartDate = DateTime.UtcNow,
                SubscriptionEndDate = DateTime.UtcNow.AddMonths(1)
            };
            await _uow.Centers.AddAsync(entity, ct);
            await _uow.SaveAsync(ct);
            return entity;
        }

        public Task<Center?> GetAsync(long id, CancellationToken ct = default)
            => _uow.Centers.GetByIdAsync(id, ct);

        public async Task<IEnumerable<Center>> SearchAsync(string? q, CancellationToken ct = default)
        {
            q ??= string.Empty;
            return await _uow.Centers.GetAsync(x => x.Name.Contains(q) || (x.Address ?? "").Contains(q), ct: ct);
        }

        public async Task<bool> UpdateAsync(long id, CenterUpdateDto dto, CancellationToken ct = default)
        {
            var entity = await _uow.Centers.GetByIdAsync(id, ct);
            if (entity is null) return false;

            entity.Name = dto.Name;
            entity.Address = dto.Address;
            entity.Phone = dto.Phone;
            entity.Email = dto.Email;
            entity.IsActive = dto.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;

            _uow.Centers.Update(entity);
            await _uow.SaveAsync(ct);
            return true;
        }

        public async Task<bool> ExtendSubscriptionAsync(long centerId, TimeSpan by, CancellationToken ct = default)
        {
            var center = await _uow.Centers.GetByIdAsync(centerId, ct);
            if (center is null) return false;

            center.SubscriptionEndDate = center.SubscriptionEndDate.Add(by);
            center.UpdatedAt = DateTime.UtcNow;
            _uow.Centers.Update(center);
            await _uow.SaveAsync(ct);
            return true;
        }
    }
}