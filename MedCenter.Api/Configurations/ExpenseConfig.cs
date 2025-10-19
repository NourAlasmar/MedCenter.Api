// هذا الكلاس يمثل إعدادات تكوين جدول المصاريف (Expenses) في قاعدة البيانات
// الغرض منه هو تخزين جميع أنواع المصاريف التشغيلية للمراكز الطبية مثل (إيجار، كهرباء، ضرائب، طعام...).
// كل سجل في هذا الجدول يمثل مصروفًا واحدًا مع تفاصيل مثل الاسم، النوع، طريقة الدفع، الحالة، وتاريخ الاستحقاق.
// هذا الجدول يُستخدم في النظام المالي للمركز لإدارة المصاريف، التذكير بالدفعات المستحقة، وحساب صافي الأرباح الشهرية.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class ExpenseConfig : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> b)
        {
            // تطبيق الإعدادات العامة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "Expenses");

            // العمود ExpenseName يُمثل اسم المصروف (مثل "إيجار العيادة" أو "شراء مواد تنظيف")
            // مطلوب (Required) بطول أقصى 150 حرفًا
            b.Property(x => x.ExpenseName).IsRequired().HasMaxLength(150);

            // العمود Category يُمثل نوع المصروف (طعام، إيجار، ضريبة... إلخ)
            // يتم تحويله من enum إلى byte للتخزين كرقم صغير لتقليل حجم البيانات
            b.Property(x => x.Category).HasConversion<byte>();

            // العمود PaymentMethod لتخزين طريقة الدفع (نقدًا، تحويل بنكي، شيك، إلخ)
            // قابل لأن يكون فارغًا (nullable) ويتم تخزينه كـ byte صغير
            b.Property(x => x.PaymentMethod).HasConversion<byte?>();

            // العمود Status لتخزين حالة الدفع (مدفوع، غير مدفوع، متأخر...)
            // يتم تخزينه كـ byte أيضًا لأنه عادة ما يكون قيمة رقمية صغيرة
            b.Property(x => x.Status).HasConversion<byte>();

            // العمود DueDate لتخزين تاريخ استحقاق المصروف
            // يُستخدم لتذكير الإدارة أو المستخدم قبل حلول موعد الدفع
            b.Property(x => x.DueDate).HasColumnType("date");

            // إنشاء فهرس مركب (Composite Index) لتحسين أداء الاستعلامات
            // الفهرس يجمع بين CenterId و Status و DueDate
            // الهدف منه تسريع عمليات البحث عن المصاريف المستحقة أو غير المسددة لمركز معين
            b.HasIndex(x => new { x.CenterId, x.Status, x.DueDate });
        }
    }
}