// هذا الكلاس يمثل إعدادات تكوين جدول العلاجات أو الإجراءات الطبية المنفذة (Treatments) في قاعدة البيانات
// الغرض منه هو تخزين كل إجراء طبي تم تنفيذه فعليًا على مريض معين من قبل طبيب داخل المركز.
// الجدول يُستخدم لتوثيق الأعمال المنجزة مثل (معاينة، حشوة، علاج جلدي، عملية... إلخ).
// كما يدعم تخصصات متعددة مثل الأسنان، الجلدية، التجميل، وغيرها، مع حقول مخصصة مثل ToothCode للأسنان و AreaCode للمناطق التشريحية.
// يُستخدم في السجل الطبي للمريض، وفي تقارير الأطباء والإدارة المالية لحساب الإيرادات بناءً على العلاجات المنفذة.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class TreatmentConfig : IEntityTypeConfiguration<Treatment>
    {
        public void Configure(EntityTypeBuilder<Treatment> b)
        {
            // تطبيق الإعدادات العامة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "Treatments");

            // العمود ToothCode يُستخدم لتحديد السن المعالج (خاص بعيادات الأسنان)
            // مثل: "UR6" (Upper Right 6) أو "LL4" (Lower Left 4)
            // اختياري بطول أقصى 10 أحرف
            b.Property(x => x.ToothCode).HasMaxLength(10);

            // العمود AreaCode يُستخدم لتحديد المنطقة في الجسم (للجلدية أو التجميل مثلاً)
            // مثل: "Face.LeftCheek" أو "Forehead.Center"
            // اختياري بطول أقصى 20 حرفًا
            b.Property(x => x.AreaCode).HasMaxLength(20);

            // العمود ExecutedAt يُمثل تاريخ ووقت تنفيذ الإجراء الطبي فعليًا
            // يُستخدم نوع datetime2(3) لتخزين التاريخ بدقة أجزاء من الثانية
            b.Property(x => x.ExecutedAt).HasColumnType("datetime2(3)");

            // إنشاء فهرس (Index) على PatientId و ExecutedAt
            // الهدف: تسريع البحث عن العلاجات الخاصة بمريض معين بترتيب زمني
            b.HasIndex(x => new { x.PatientId, x.ExecutedAt });

            // إنشاء فهرس آخر على DoctorId و ExecutedAt
            // الهدف: تسريع الاستعلامات الخاصة بجلب جميع الإجراءات التي نفذها طبيب معين ضمن فترة زمنية محددة
            // هذا يُفيد في صفحة الطبيب وفي التقارير التشغيلية والإحصائية
            b.HasIndex(x => new { x.DoctorId, x.ExecutedAt });
        }
    }
}