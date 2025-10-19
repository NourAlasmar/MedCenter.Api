// هذا الكلاس يمثل إعدادات تكوين جدول تفاصيل الفواتير (InvoiceItems) في قاعدة البيانات
// الغرض منه هو تخزين تفاصيل كل بند أو خدمة مدرجة داخل الفاتورة.
// كل سجل في هذا الجدول يمثل خدمة أو إجراء واحد مضاف إلى الفاتورة (مثل "حشوة أسنان"، "استشارة طبية"، "تحاليل مخبرية").
// الجدول يرتبط بشكل مباشر بجدول الفواتير (Invoices) من خلال المفتاح الأجنبي InvoiceId.
// يساعد هذا الجدول في عرض الفواتير بالتفصيل وحساب الإجمالي والخصومات والضرائب.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class InvoiceItemConfig : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> b)
        {
            // تطبيق الإعدادات العامة المشتركة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "InvoiceItems");

            // العمود Description يُمثل وصف البند أو الخدمة (مثلاً "معاينة عامة" أو "حشوة ضرس علوي")
            // تم تعيينه كمطلوب (Required) بطول أقصى 300 حرف
            b.Property(x => x.Description).IsRequired().HasMaxLength(300);

            // إنشاء فهرس (Index) على العمود InvoiceId
            // الهدف: تسريع عمليات البحث عن جميع البنود المرتبطة بفاتورة معينة
            // هذا مفيد عند عرض تفاصيل الفاتورة أو عند إنشاء تقارير مالية دقيقة
            b.HasIndex(x => x.InvoiceId);
        }
    }
}