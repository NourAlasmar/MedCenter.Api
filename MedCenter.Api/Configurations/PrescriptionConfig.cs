// هذا الكلاس يمثل إعدادات تكوين جدول الوصفات الطبية (Prescriptions) في قاعدة البيانات
// الغرض منه هو تخزين جميع الوصفات الطبية التي يقوم الأطباء بإصدارها للمرضى.
// كل سجل يمثل وصفة واحدة تحتوي على تفاصيل الأدوية، الجرعات، الطبيب الذي أصدرها، والمريض المستفيد منها.
// الجدول يُستخدم في صفحات الطبيب والمريض والمحاسبة، كما يُمكن ربطه بصفحة الإشعارات لإرسال الوصفة عبر الرسائل القصيرة أو البريد الإلكتروني.
// هذا الجدول يُعتبر جزءًا أساسيًا من السجل الطبي الإلكتروني (EMR) الخاص بالنظام.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class PrescriptionConfig : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> b)
        {
            // تطبيق الإعدادات العامة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "Prescriptions");

            // العمود IssuedAt يُمثل تاريخ ووقت إصدار الوصفة الطبية من قبل الطبيب
            // يُستخدم نوع datetime2(3) لتخزين الوقت بدقة أجزاء من الثانية
            // هذا الحقل يُفيد في معرفة توقيت آخر وصفة صدرت للمريض ومتابعة تاريخه العلاجي
            b.Property(x => x.IssuedAt).HasColumnType("datetime2(3)");
        }
    }
}