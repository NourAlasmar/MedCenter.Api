// هذا الكلاس يمثل إعدادات تكوين جدول البيانات المالية (FinancialStatements) في قاعدة البيانات
// الغرض منه هو تخزين التقارير المالية الدورية التي يتم توليدها من النظام.
// كل سجل في هذا الجدول يمثل تقريرًا ماليًا لفترة محددة (شهرية، ربع سنوية، سنوية...)
// يحتوي التقرير على ملخصات مثل: الإيرادات، المصروفات، الأرباح الصافية، والضرائب.
// يُستخدم هذا الجدول في قسم الإدارة المالية للمركز لعرض وتحليل الأداء المالي بمرور الوقت.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class FinancialStatementConfig : IEntityTypeConfiguration<FinancialStatement>
    {
        public void Configure(EntityTypeBuilder<FinancialStatement> b)
        {
            // تطبيق الإعدادات العامة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "FinancialStatements");

            // العمود PeriodStart يُخزن تاريخ بداية الفترة المالية للتقرير (مثلاً 01/01/2025)
            // نوع العمود هو date لأننا نحتاج التاريخ فقط بدون وقت
            b.Property(x => x.PeriodStart).HasColumnType("date");

            // العمود PeriodEnd يُخزن تاريخ نهاية الفترة المالية للتقرير (مثلاً 31/01/2025)
            // أيضًا من نوع date لتحديد نهاية الفترة المحاسبية للتقرير
            b.Property(x => x.PeriodEnd).HasColumnType("date");

            // العمود GeneratedAt يُمثل تاريخ ووقت توليد التقرير المالي فعليًا
            // تم تحديد نوعه كـ datetime2(3) لتخزين الوقت بدقة أجزاء من الثانية
            b.Property(x => x.GeneratedAt).HasColumnType("datetime2(3)");
        }
    }
}