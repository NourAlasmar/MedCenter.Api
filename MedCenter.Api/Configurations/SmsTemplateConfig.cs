// هذا الكلاس يمثل إعدادات تكوين جدول قوالب الرسائل النصية (SmsTemplates) في قاعدة البيانات
// الغرض منه هو تخزين الرسائل الجاهزة (Templates) التي يمكن للمركز استخدامها بشكل متكرر عند إرسال SMS.
// مثل رسائل التذكير بالمواعيد، إشعارات الدفع، تجديد الاشتراك، أو الترحيب بالمريض.
// كل سجل في هذا الجدول يمثل قالب رسالة يحتوي على مفتاح تعريف (Key) ونص الرسالة (Body).
// الجدول يُستخدم مع خدمة الإرسال (SmsOutbox) لتوليد رسائل تلقائية بناءً على الأحداث داخل النظام.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class SmsTemplateConfig : IEntityTypeConfiguration<SmsTemplate>
    {
        public void Configure(EntityTypeBuilder<SmsTemplate> b)
        {
            // تطبيق الإعدادات العامة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "SmsTemplates");

            // العمود Key يُمثل المفتاح الفريد للقالب (مثل: AppointmentReminder, PaymentDue, WelcomeMessage)
            // مطلوب (Required) بطول أقصى 50 حرف
            b.Property(x => x.Key).IsRequired().HasMaxLength(50);

            // العمود Body يُخزن نص الرسالة الجاهزة التي سيتم إرسالها
            // مطلوب (Required) بطول أقصى 500 حرف لتغطية النصوص الطويلة
            b.Property(x => x.Body).IsRequired().HasMaxLength(500);

            // إنشاء فهرس (Index) فريد يجمع بين CenterId و Key
            // الهدف: منع تكرار نفس القالب داخل نفس المركز، مع السماح لمراكز مختلفة بإنشاء قوالب تحمل نفس المفتاح
            b.HasIndex(x => new { x.CenterId, x.Key }).IsUnique();
        }
    }
}