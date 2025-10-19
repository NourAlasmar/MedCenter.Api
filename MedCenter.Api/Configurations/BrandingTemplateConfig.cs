// هذا الكلاس يمثل إعدادات تكوين جدول قوالب الهوية البصرية (BrandingTemplates) في قاعدة البيانات
// الهدف منه هو حفظ القوالب الخاصة بالمراكز الطبية، مثل:
// شعار المركز، تصميم الفواتير، الألوان، الهيدر والفوتر في الملفات المطبوعة أو المرسلة.
// هذا الجدول يسمح لكل مركز طبي بإنشاء أكثر من قالب (مثلاً قالب للفواتير وآخر للوصفات الطبية)
// مع تحديد القالب النشط (Active) المستخدم حاليًا.
// هذه الإعدادات تُستخدم لاحقًا لتوليد مستندات وفواتير تحمل الهوية البصرية الخاصة بكل مركز.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class BrandingTemplateConfig : IEntityTypeConfiguration<BrandingTemplate>
    {
        public void Configure(EntityTypeBuilder<BrandingTemplate> b)
        {
            CommonCfg.Base(b, "BrandingTemplates");

            // تحويل نوع القالب (Type) من enum إلى byte لتخزينه كرقم صغير في قاعدة البيانات
            // مثلاً: 1 = فاتورة، 2 = وصفة طبية، 3 = تقرير مختبر، إلخ
            b.Property(x => x.Type).HasConversion<byte>();

            // العمود TemplateJson يحتوي على التصميم بصيغة JSON (ملف قالب يحتوي تفاصيل التصميم)
            // تم تحديده كـ Required لأنه لا يمكن أن يكون القالب فارغًا
            b.Property(x => x.TemplateJson).IsRequired();

            // إنشاء  مركّب يجمع بين CenterId و Type و Active
            // الهدف: ضمان عدم وجود أكثر من قالب نشط (Active = 1) من نفس النوع داخل نفس المركز
            // (أي أن كل مركز يمكن أن يملك قالبًا نشطًا واحدًا فقط من كل نوع)
            // تمت إضافة HasFilter لتطبيق شرط الفهرس فقط عندما Active = 1
            b.HasIndex(x => new { x.CenterId, x.Type, x.Active })
             .HasFilter("[Active] = 1")  // الشرط يُطبّق فقط على القوالب النشطة
             .IsUnique();                 // يضمن عدم التكرار للقوالب النشطة من نفس النوع
        }
    }
}