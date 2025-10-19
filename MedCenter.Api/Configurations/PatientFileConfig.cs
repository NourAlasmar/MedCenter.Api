// هذا الكلاس يمثل إعدادات تكوين جدول ملفات المرضى (PatientFiles) في قاعدة البيانات
// الغرض منه هو تخزين الملفات والمرفقات الخاصة بكل مريض داخل المركز الطبي.
// مثل الصور الطبية (أشعة – سونار – تقارير)، ملفات PDF، نتائج تحاليل، أو صور قبل وبعد العلاج.
// كل سجل يمثل ملفًا واحدًا مرفوعًا يخص مريضًا محددًا، ويمكن ربطه بالمركز والطبيب المعالج.
// الجدول يُستخدم في صفحات المريض والأطباء والمختبر، لتسهيل الوصول إلى الملفات أثناء التشخيص أو المتابعة.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class PatientFileConfig : IEntityTypeConfiguration<PatientFile>
    {
        public void Configure(EntityTypeBuilder<PatientFile> b)
        {
            // تطبيق الإعدادات العامة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "PatientFiles");

            // العمود FileName يُمثل اسم الملف الفعلي (مثل "XRay_2025_01_18.jpg")
            // مطلوب (Required) بطول أقصى 260 حرف، وهو الحد القياسي لأسماء الملفات في أنظمة ويندوز
            b.Property(x => x.FileName).IsRequired().HasMaxLength(260);

            // العمود StorageUrl يُمثل الرابط الكامل للموقع الذي تم حفظ الملف فيه (محليًا أو سحابيًا)
            // مطلوب (Required) بطول أقصى 500 حرف لتغطية روابط التخزين الطويلة
            b.Property(x => x.StorageUrl).IsRequired().HasMaxLength(500);

            // العمود FileType يُحدد نوع الملف (مثل "image/jpeg", "application/pdf", "video/mp4")
            // اختياري بطول أقصى 50 حرفًا
            b.Property(x => x.FileType).HasMaxLength(50);

            // إنشاء فهرس (Index) يجمع بين PatientId و CenterId
            // الهدف: تسريع عمليات البحث عن الملفات الخاصة بمريض داخل مركز معين
            // كما يسهل عمليات الفلترة في لوحة الطبيب أو الإدارة
            b.HasIndex(x => new { x.PatientId, x.CenterId });
        }
    }
}