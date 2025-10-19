// هذا الكلاس يمثل إعدادات تكوين جدول الخدمات المخبرية (LabServices) في قاعدة البيانات
// الغرض منه هو تعريف قائمة التحاليل أو الإجراءات المخبرية التي يقدمها كل مركز أو مخبر.
// كل سجل يمثل خدمة واحدة يمكن للطبيب طلبها (مثل "تحليل سكر الدم"، "فحص الكولسترول"، "تركيب سن").
// الجدول يُستخدم من قبل الطبيب عند إنشاء طلب للمخبر (LabOrder)، كما يمكن للمركز تحديد الأسعار الخاصة بكل خدمة.
// هذا يساعد في إدارة العروض، التسعير، وتتبع أكثر الخدمات طلبًا في النظام.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class LabServiceConfig : IEntityTypeConfiguration<LabService>
    {
        public void Configure(EntityTypeBuilder<LabService> b)
        {
            // تطبيق الإعدادات العامة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "LabServices");

            // العمود Name يُمثل اسم الخدمة المخبرية (مثل "تحليل دم شامل" أو "زراعة بكتيرية")
            // تم تعيينه كمطلوب (Required) بطول أقصى 200 حرف
            b.Property(x => x.Name).IsRequired().HasMaxLength(200);

            // إنشاء فهرس (Index) فريد يجمع بين CenterId و Name
            // الهدف: منع تكرار نفس اسم الخدمة داخل نفس المركز
            // يسمح بأن تكون الخدمة نفسها موجودة في مراكز أخرى دون تعارض
            b.HasIndex(x => new { x.CenterId, x.Name }).IsUnique();
        }
    }
}