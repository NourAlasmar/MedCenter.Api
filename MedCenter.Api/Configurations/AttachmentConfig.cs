// هذا الكلاس يمثل إعدادات تكوين جدول المرفقات (Attachments) في قاعدة البيانات
// الغرض منه هو تعريف شكل الجدول الذي يُخزن فيه الملفات المرفقة داخل النظام
// مثل صور المرضى، العقود، الفواتير، أو أي ملفات مرفوعة من المستخدمين أو المراكز الطبية
// كل مرفق يحتوي على اسم الملف، المسار (URL)، ونوعه (MIME type) بالإضافة إلى معرف المركز المرتبط به

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class AttachmentConfig : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> b)
        {
            CommonCfg.Base(b, "Attachments");

            // تحديد أن اسم الملف FileName مطلوب (Required)
            // أقصى طول 260 حرفًا (يتماشى مع قيود نظام الملفات في Windows)
            b.Property(x => x.FileName).IsRequired().HasMaxLength(260);

            // تحديد أن مسار الملف (URL) مطلوب أيضًا
            // هذا المسار قد يكون رابط تخزين محلي أو على خدمة تخزين سحابية مثل Azure Blob أو AWS S3
            b.Property(x => x.Url).IsRequired().HasMaxLength(500);

            // تحديد نوع الملف (MIME) مثل image/png أو application/pdf
            b.Property(x => x.Mime).HasMaxLength(80);

            // إنشاء  (Index) على CenterId لتحسين أداء الاستعلامات التي تبحث عن المرفقات الخاصة بمركز طبي معين
            b.HasIndex(x => x.CenterId);
        }
    }
}