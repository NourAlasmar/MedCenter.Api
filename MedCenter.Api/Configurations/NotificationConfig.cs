// هذا الكلاس يمثل إعدادات تكوين جدول الإشعارات (Notifications) في قاعدة البيانات
// الغرض منه هو إدارة وتخزين جميع الإشعارات التي تُرسل للمستخدمين داخل النظام.
// يمكن أن تكون الإشعارات خاصة بتذكير المواعيد، انتهاء الاشتراكات، رسائل من الإدارة، أو تنبيهات أمنية.
// الجدول يحتوي على تفاصيل الإشعار مثل العنوان، المحتوى، نوع الإشعار، حالة القراءة، وتاريخ الإنشاء.
// هذا الكلاس يُستخدم مع خدمة الإشعارات (Notification Service) لإرسال وإدارة التنبيهات في الواجهة الأمامية (Front-End).

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class NotificationConfig : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> b)
        {
            // تطبيق الإعدادات العامة الموحدة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "Notifications");

            // العمود Type يُحدد نوع الإشعار (مثل: تذكير، تنبيه، إشعار مالي، إشعار نظام)
            // يتم تحويله من enum إلى byte لتخزينه كقيمة رقمية صغيرة داخل قاعدة البيانات
            b.Property(x => x.Type).HasConversion<byte>();

            // العمود CreatedAtUtc يُمثل وقت إنشاء الإشعار بتوقيت UTC
            // يُخزن كـ datetime2(3) لتوفير دقة زمنية عالية تصل إلى أجزاء من الثانية
            b.Property(x => x.CreatedAtUtc).HasColumnType("datetime2(3)");

            // العمود Title يُخزن عنوان الإشعار (مختصر ومباشر)
            // مطلوب (Required) بطول أقصى 150 حرفًا
            b.Property(x => x.Title).IsRequired().HasMaxLength(150);

            // العمود Message يُمثل نص الإشعار أو محتواه الكامل
            // مطلوب (Required) بطول أقصى 1000 حرف لتغطية النصوص الطويلة
            b.Property(x => x.Message).IsRequired().HasMaxLength(1000);

            // إنشاء فهرس (Index) على UserId و IsRead
            // الهدف: تسريع البحث عن الإشعارات الخاصة بمستخدم معين سواء كانت مقروءة أم لا
            // يُستخدم هذا الفهرس في واجهة المستخدم عند عرض الإشعارات الجديدة فقط
            b.HasIndex(x => new { x.UserId, x.IsRead });
        }
    }
}