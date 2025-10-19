// هذا الكلاس يمثل إعدادات تكوين جدول الملاحظات والتغذية الراجعة (Feedback) في قاعدة البيانات
// الغرض منه هو تخزين الملاحظات أو الشكاوى أو الاقتراحات التي يقدمها المستخدمون أو المرضى أو موظفو المراكز.
// يمكن أن تكون التغذية الراجعة شكوى، اقتراح تطوير، أو تقييم تجربة خدمة معينة.
// هذا الجدول يُساعد الإدارة على مراقبة جودة الخدمات وتحسين الأداء الداخلي للمراكز الطبية.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class FeedbackConfig : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> b)
        {
            // تطبيق الإعدادات العامة الموحدة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "Feedback");

            // العمود Message يحتوي على نص الملاحظة أو الشكوى أو الاقتراح
            // تم تحديده كمطلوب (Required) لأنه لا يمكن حفظ سجل بدون رسالة
            b.Property(x => x.Message).IsRequired();

            // العمود Type يُمثل نوع الملاحظة (مثلاً: "Complaint" أو "Suggestion" أو "Praise")
            // تم تحديده كمطلوب مع حد أقصى للطول 30 حرفًا
            b.Property(x => x.Type).IsRequired().HasMaxLength(30);

            // إنشاء فهرس (Index) على CenterId و Status
            // الهدف منه تسريع عمليات البحث عن الملاحظات أو الشكاوى حسب المركز أو حالة المعالجة
            // مثلاً: عرض كل الشكاوى "Pending" لمركز محدد
            b.HasIndex(x => new { x.CenterId, x.Status });
        }
    }
}