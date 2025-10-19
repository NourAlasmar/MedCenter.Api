// هذا الكلاس يمثل إعدادات تكوين جدول التذكيرات بالمواعيد (AppointmentReminders) في قاعدة البيانات
// الغرض منه هو تحديد شكل الجدول الذي يُخزن فيه بيانات التذكير بمواعيد المرضى والأطباء
// النظام يستخدم هذا الجدول لإرسال إشعارات أو رسائل تذكير (SMS / بريد إلكتروني) قبل موعد المريض

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class AppointmentReminderConfig : IEntityTypeConfiguration<AppointmentReminder>
    {
        public void Configure(EntityTypeBuilder<AppointmentReminder> b)
        {
            CommonCfg.Base(b, "AppointmentReminders");

            // تحويل نوع التذكير (Channel) من enum إلى byte لتخزينه كرقم صغير في قاعدة البيانات
            // مثلاً: 1 = SMS, 2 = Email, 3 = Notification
            b.Property(x => x.Channel).HasConversion<byte>();

            // تحويل حالة التذكير (Status) من enum إلى byte أيضًا
            // مثل: 0 = Pending, 1 = Sent, 2 = Failed
            b.Property(x => x.Status).HasConversion<byte>();

            // تحديد نوع العمود ScheduledAt ليكون datetime2(3)
            // هذا الحقل يحدد وقت جدولة إرسال التذكير (مثلاً قبل الموعد بـ 24 ساعة)
            b.Property(x => x.ScheduledAt).HasColumnType("datetime2(3)");

            // تحديد نوع العمود SentAt ليكون datetime2(3)
            // هذا الحقل يخزن وقت الإرسال الفعلي للتذكير بعد نجاح العملية
            b.Property(x => x.SentAt).HasColumnType("datetime2(3)");
        }
    }
}