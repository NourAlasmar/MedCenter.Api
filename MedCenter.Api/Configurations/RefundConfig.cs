// هذا الكلاس يمثل إعدادات تكوين جدول المرتجعات المالية (Refunds) في قاعدة البيانات
// الغرض منه هو تخزين العمليات التي يتم فيها إرجاع مبالغ مالية للمرضى أو العملاء.
// كل سجل يمثل عملية استرجاع واحدة (Refund) مرتبطة بفاتورة أو إجراء طبي تم إلغاؤه أو تعديله.
// هذا الجدول يُستخدم في النظام المالي والمحاسبة لتوثيق جميع عمليات الإرجاع لأغراض التدقيق والمراجعة.
// كما يساعد في إعداد التقارير الخاصة بالمصاريف والاسترجاعات لكل مركز طبي خلال فترات محددة.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class RefundConfig : IEntityTypeConfiguration<Refund>
    {
        public void Configure(EntityTypeBuilder<Refund> b)
        {
            // تطبيق الإعدادات العامة (المفتاح الأساسي، الحذف المنطقي، تواريخ الإنشاء والتعديل)
            CommonCfg.Base(b, "Refunds");

            // العمود RefundDate يُخزن تاريخ ووقت تنفيذ عملية الاسترجاع
            // يُستخدم datetime2(3) لتخزين الوقت بدقة أجزاء من الثانية
            b.Property(x => x.RefundDate).HasColumnType("datetime2(3)");

            // إنشاء فهرس (Index) على CenterId و RefundDate
            // الهدف: تسريع عمليات البحث عن عمليات الإرجاع الخاصة بمركز محدد ضمن فترة زمنية معينة
            // مفيد عند إنشاء تقارير مالية أسبوعية أو شهرية
            b.HasIndex(x => new { x.CenterId, x.RefundDate });
        }
    }
}