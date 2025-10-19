// هذا الكلاس يمثل إعدادات تكوين جدول تشخيصات المرضى (PatientDiagnoses) في قاعدة البيانات
// الغرض منه هو تخزين جميع التشخيصات الطبية المسجّلة لكل مريض في النظام.
// كل سجل في هذا الجدول يمثل تشخيصًا واحدًا قام الطبيب بإضافته، مع تاريخ تسجيله ونوعه.
// الجدول يُستخدم في صفحات الطبيب، السجلات الطبية، والتقارير لتتبع تطور الحالة الصحية للمريض عبر الزمن.
// كما يمكن ربطه ببيانات الإجراءات العلاجية (Treatments) أو التحاليل المخبرية لمتابعة حالة المريض بدقة.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class PatientDiagnosisConfig : IEntityTypeConfiguration<PatientDiagnosis>
    {
        public void Configure(EntityTypeBuilder<PatientDiagnosis> b)
        {
            // تطبيق الإعدادات العامة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "PatientDiagnoses");

            // العمود RecordedAt يُمثل تاريخ ووقت تسجيل التشخيص للمريض
            // يُستخدم datetime2(3) لتخزين الوقت بدقة أجزاء من الثانية
            b.Property(x => x.RecordedAt).HasColumnType("datetime2(3)");

            // إنشاء فهرس (Index) يجمع بين PatientId و RecordedAt
            // الهدف: تسريع عمليات البحث عن التشخيصات الخاصة بمريض معين بحسب التاريخ
            // مفيد جدًا عند عرض السجل الطبي الزمني للمريض أو إعداد التقارير الدورية
            b.HasIndex(x => new { x.PatientId, x.RecordedAt });
        }
    }
}