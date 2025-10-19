//  واجهة خدمة المراكز الطبية (ICenterService)
//
// الغرض:
// تعريف العمليات الأساسية لإدارة المراكز الطبية داخل النظام.
// هذه الواجهة تُستخدم كعقد (Contract) بين المنفذ (CenterService)
// وبقية أجزاء التطبيق (مثل Controllers أو الخدمات الأخرى).
//
// الفوائد:
//  تسهّل اختبار الكود (Unit Testing) عبر الاعتماد على واجهة بدلًا من تنفيذ فعلي.
//  تسمح باستبدال أو تطوير منطق الخدمة دون كسر بقية النظام.
//  تفصل منطق العمل (Business Logic) عن طبقة التحكم (API Controllers).
//
// العمليات:
//
//  CreateAsync : إنشاء مركز جديد
//     - تُنشئ كائن Center جديد في قاعدة البيانات.
//     - تُستخدم عند اشتراك مركز جديد في النظام.
//
//  GetAsync : جلب بيانات مركز محدد
//     - تُعيد تفاصيل المركز عبر الـ Id.
//
// SearchAsync : البحث عن المراكز
//     - تُعيد قائمة المراكز التي تحتوي أسماؤها أو عناوينها على النص المُدخل (q).
//     - تُفيد في لوحة التحكم أو البحث العام.
//
//  UpdateAsync : تحديث بيانات مركز قائم
//     - لتعديل الاسم، العنوان، الهاتف، البريد الإلكتروني، أو حالة النشاط (IsActive).
//
//  ExtendSubscriptionAsync : تمديد الاشتراك
//     - تُضيف مدة زمنية (TimeSpan) إلى تاريخ انتهاء الاشتراك الحالي.
//     - تُستخدم عند تجديد اشتراك المركز.

using MedCenter.Api.DTOs;
using MedCenter.Api.Models;

namespace MedCenter.Api.Services.Interfaces
{
    public interface ICenterService
    {
        Task<Center> CreateAsync(CenterCreateDto dto, CancellationToken ct = default);
        Task<Center?> GetAsync(long id, CancellationToken ct = default);
        Task<IEnumerable<Center>> SearchAsync(string? q, CancellationToken ct = default);
        Task<bool> UpdateAsync(long id, CenterUpdateDto dto, CancellationToken ct = default);
        Task<bool> ExtendSubscriptionAsync(long centerId, TimeSpan by, CancellationToken ct = default);
    }
}