// هذا الكلاس يمثل إعدادات تكوين جدول المواعيد (Appointments) في قاعدة البيانات
// الغرض منه هو تعريف شكل الجدول في قاعدة البيانات والعلاقات والفهارس الخاصة به.
// الجدول يُستخدم لتخزين مواعيد الأطباء مع المرضى داخل كل مركز طبي
// يحتوي على معلومات مثل نوع الموعد (معاينة، عملية...)، حالته (مؤكد، ملغي...)، وتاريخ ومدة الموعد

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class AppointmentConfig : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> b)
        {
            CommonCfg.Base(b, "Appointments");

            // تحويل نوع البيانات enum الخاص بـ Type إلى byte عند التخزين في قاعدة البيانات
            // هذا يجعل الجدول يستخدم أرقام صغيرة بدل النصوص (أكثر كفاءة في المساحة)
            b.Property(x => x.Type).HasConversion<byte>();

            // تحويل حالة الموعد (enum Status) أيضًا إلى byte للتخزين في قاعدة البيانات
            b.Property(x => x.Status).HasConversion<byte>();

            // تحديد نوع العمود AppointmentDate ليكون datetime2(3) لتخزين الوقت بدقة جزء من الثانية
            b.Property(x => x.AppointmentDate).HasColumnType("datetime2(3)");

            // إنشاء  (Index) على DoctorId و AppointmentDate
            // الهدف: تسريع عمليات البحث عن مواعيد طبيب محدد خلال فترة زمنية معينة
            b.HasIndex(x => new { x.DoctorId, x.AppointmentDate });
        }
    }
}