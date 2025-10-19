// هذا الكلاس يمثل إعدادات تكوين جدول صندوق إرسال الرسائل القصيرة (SmsOutbox) في قاعدة البيانات
// الغرض منه هو تخزين جميع الرسائل النصية (SMS) التي يتم إرسالها من النظام إلى المستخدمين (مثل المرضى أو الأطباء).
// كل سجل في هذا الجدول يمثل رسالة واحدة تحتوي على رقم المرسل إليه، الحالة، توقيت الإرسال، ومصدر الرسالة.
// الجدول يُستخدم لتتبع حالة الإرسال (قيد الإرسال، تم الإرسال، فشل...) ولأغراض المراجعة والإحصائيات.
// كما يمكن استخدامه في لوحة التحكم لعرض السجل الزمني للرسائل المرسلة من المركز الطبي.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class SmsOutboxConfig : IEntityTypeConfiguration<SmsOutbox>
    {
        public void Configure(EntityTypeBuilder<SmsOutbox> b)
        {
            // تطبيق الإعدادات العامة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "SmsOutbox");

            // العمود ToPhone يُمثل رقم الهاتف الذي ستُرسل إليه الرسالة
            // مطلوب (Required) بطول أقصى 30 حرف لتغطية جميع صيغ الأرقام المحلية والدولية
            b.Property(x => x.ToPhone).IsRequired().HasMaxLength(30);

            // العمود Status يُحدد حالة الرسالة (قيد الإرسال، تم الإرسال، فشل...)
            // يتم تحويله من enum إلى byte لتخزينه كقيمة رقمية صغيرة
            b.Property(x => x.Status).HasConversion<byte>();

            // العمود ScheduledAt يُمثل الوقت المحدد لإرسال الرسالة (في حال كانت مجدولة)
            // يُستخدم datetime2(3) لتوفير دقة زمنية عالية
            b.Property(x => x.ScheduledAt).HasColumnType("datetime2(3)");

            // العمود SentAt يُمثل الوقت الفعلي الذي تم فيه إرسال الرسالة
            // يُخزن أيضًا بدقة أجزاء من الثانية
            b.Property(x => x.SentAt).HasColumnType("datetime2(3)");

            // إنشاء فهرس (Index) يجمع بين CenterId و Status
            // الهدف: تسريع عمليات البحث عن الرسائل المرسلة أو المعلقة الخاصة بمركز معين
            // هذا الفهرس مفيد لتقارير الأداء والمتابعة اللحظية للإرسال
            b.HasIndex(x => new { x.CenterId, x.Status });
        }
    }
}