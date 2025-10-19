// هذا الكلاس يمثل إعدادات تكوين جدول موافقات المرضى (PatientConsents) في قاعدة البيانات
// الغرض منه هو تخزين جميع النماذج والموافقات التي يوقّعها المرضى أثناء أو قبل تلقي الخدمة الطبية.
// مثل موافقة على إجراء عملية، أو تصوير، أو مشاركة بيانات طبية، أو استخدام علاج معين.
// كل سجل في هذا الجدول يمثل موافقة واحدة مع نوعها، وتاريخ التوقيع، ورابط المستند الموقّع.
// يُستخدم هذا الجدول في صفحات الطبيب والإدارة القانونية لتوثيق جميع الإجراءات المرتبطة بالمريض.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class PatientConsentConfig : IEntityTypeConfiguration<PatientConsent>
    {
        public void Configure(EntityTypeBuilder<PatientConsent> b)
        {
            // تطبيق الإعدادات العامة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "PatientConsents");

            // العمود ConsentType يُمثل نوع الموافقة (مثل: "موافقة على الجراحة" أو "موافقة على التحليل")
            // مطلوب (Required) بطول أقصى 100 حرف
            b.Property(x => x.ConsentType).IsRequired().HasMaxLength(100);

            // العمود SignedAt يُخزن تاريخ ووقت توقيع المريض على الموافقة
            // يُستخدم datetime2(3) لتوفير دقة زمنية عالية تصل لأجزاء من الثانية
            b.Property(x => x.SignedAt).HasColumnType("datetime2(3)");

            // العمود DocumentUrl يُخزن الرابط المؤدي إلى نسخة المستند الموقّع إلكترونيًا أو الممسوح ضوئيًا
            // الحد الأقصى للطول 500 حرف لتغطية روابط السحابة أو السيرفر الداخلي
            b.Property(x => x.DocumentUrl).HasMaxLength(500);
        }
    }
}