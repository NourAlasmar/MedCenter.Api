// هذا الكلاس يمثل إعدادات تكوين جدول مستندات الموظفين (EmployeeDocuments) في قاعدة البيانات
// الغرض منه هو تخزين الملفات والمستندات المرفقة الخاصة بكل موظف في النظام
// مثل السيرة الذاتية (CV)، الهوية الشخصية، الشهادات، العقود الموقعة، أو أي ملفات أخرى مرتبطة بالموظف.
// هذا الجدول يُستخدم في قسم الموارد البشرية (HR) لإدارة وتنظيم الملفات الإلكترونية للموظفين بدلاً من حفظها ورقيًا.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class EmployeeDocumentConfig : IEntityTypeConfiguration<EmployeeDocument>
    {
        public void Configure(EntityTypeBuilder<EmployeeDocument> b)
        {
            // تطبيق الإعدادات العامة الأساسية مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "EmployeeDocuments");

            // العمود FileName يُخزن اسم الملف المرفوع (مثل CV.pdf أو ID_Card.jpg)
            // مطلوب (Required) بطول أقصى 260 حرفًا، وهو الطول القياسي لأسماء الملفات في Windows
            b.Property(x => x.FileName).IsRequired().HasMaxLength(260);

            // العمود Url يُخزن المسار الكامل أو الرابط المباشر للملف (محلي أو على التخزين السحابي)
            // مطلوب (Required) بطول أقصى 500 حرف
            b.Property(x => x.Url).IsRequired().HasMaxLength(500);

            // العمود Mime يُخزن نوع الملف MIME type مثل:
            // image/jpeg أو application/pdf لتحديد نوع المحتوى عند العرض أو التحميل
            b.Property(x => x.Mime).HasMaxLength(80);

            // إنشاء فهرس (Index) على EmployeeId لتحسين أداء الاستعلامات
            // الفهرس يجعل البحث عن مستندات موظف معين أسرع وأكثر كفاءة
            b.HasIndex(x => x.EmployeeId);
        }
    }
}