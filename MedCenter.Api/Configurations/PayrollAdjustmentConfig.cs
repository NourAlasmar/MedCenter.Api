// هذا الكلاس يمثل إعدادات تكوين جدول التعديلات على الرواتب (PayrollAdjustments) في قاعدة البيانات
// الغرض منه هو تسجيل جميع التعديلات التي تُجرى على رواتب الموظفين مثل:
// (الخصومات، المكافآت، الساعات الإضافية، الحوافز، أو أي تعديل إداري).
// كل سجل في هذا الجدول يربط موظفًا بشهر وسنة محددين مع نوع التعديل وقيمته.
// الجدول يُستخدم في النظام المالي (قسم الرواتب) لحساب الراتب النهائي لكل موظف بدقة قبل عملية الدفع.
// كما يُستخدم في التقارير الشهرية لمتابعة مجموع المكافآت والخصومات داخل المركز الطبي.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class PayrollAdjustmentConfig : IEntityTypeConfiguration<PayrollAdjustment>
    {
        public void Configure(EntityTypeBuilder<PayrollAdjustment> b)
        {
            // تطبيق الإعدادات العامة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "PayrollAdjustments");

            // العمود Type يُحدد نوع التعديل على الراتب (خصم، مكافأة، علاوة، إلخ)
            // يتم تخزينه كـ tinyint لتقليل حجم البيانات واستخدام قيم رقمية صغيرة
            b.Property(x => x.Type).HasColumnType("tinyint");

            // إنشاء فهرس (Index) مركب يجمع بين EmployeeId و Year و Month
            // الهدف: تسريع الاستعلامات الخاصة بجلب تعديلات الراتب لموظف معين في شهر محدد
            // كما يساعد على منع التكرار لنفس الموظف في نفس الفترة الزمنية
            b.HasIndex(x => new { x.EmployeeId, x.Year, x.Month });
        }
    }
}