// هذا الكلاس يمثل إعدادات تكوين جدول الملاحظات الخاصة بالموظفين (EmployeeNotes) في قاعدة البيانات
// الغرض منه هو تخزين الملاحظات الإدارية أو التقييمية التي تُضاف على الموظفين داخل النظام.
// مثل: "تأخر عن الدوام"، "أداء ممتاز في الشهر الماضي"، "يحتاج إلى تدريب إضافي"، إلخ.
// هذا الجدول يُستخدم عادة من قبل مدير الموارد البشرية أو المشرف لتتبع سلوك الموظفين وأدائهم.
// يساعد أيضًا في إعداد التقييم السنوي أو عند اتخاذ قرارات الترقيات والمكافآت.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class EmployeeNoteConfig : IEntityTypeConfiguration<EmployeeNote>
    {
        public void Configure(EntityTypeBuilder<EmployeeNote> b)
        {
            // تطبيق الإعدادات العامة الأساسية مثل المفتاح الأساسي، تواريخ الإنشاء والتعديل، والحذف المنطقي
            CommonCfg.Base(b, "EmployeeNotes");

            // العمود Note يحتوي على نص الملاحظة المكتوبة من قبل الإدارة أو المشرف
            // تم تحديده كمطلوب (Required) لأن كل ملاحظة يجب أن تحتوي على نص فعلي
            b.Property(x => x.Note).IsRequired();

            // إنشاء فهرس (Index) على EmployeeId لتسريع عمليات البحث
            // الهدف: تسهيل الاستعلام عن جميع الملاحظات المرتبطة بموظف معين بسرعة وكفاءة
            b.HasIndex(x => x.EmployeeId);
        }
    }
}