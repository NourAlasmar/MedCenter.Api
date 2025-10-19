// هذا الكلاس يمثل إعدادات تكوين جدول مهام الطباعة (PrintJobs) في قاعدة البيانات
// الغرض منه هو تسجيل جميع العمليات التي يتم فيها طباعة المستندات أو التقارير أو الفواتير داخل النظام.
// كل سجل في هذا الجدول يمثل عملية طباعة واحدة تتضمن الكيان الذي تمت طباعته (فاتورة، وصفة، تقرير...)
// بالإضافة إلى هوية المركز، وتاريخ الطباعة، لتوثيق كل عملية لأغراض الأمان والتتبع.
// الجدول يُستخدم في قسم الإدارة والتدقيق (Audit) لمعرفة من قام بطباعة ماذا ومتى، مما يعزز الشفافية والرقابة.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class PrintJobConfig : IEntityTypeConfiguration<PrintJob>
    {
        public void Configure(EntityTypeBuilder<PrintJob> b)
        {
            // تطبيق الإعدادات العامة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "PrintJobs");

            // العمود Entity يُمثل نوع الكيان الذي تمت طباعته (مثل "Invoice", "Prescription", "Report")
            // مطلوب (Required) بطول أقصى 50 حرف لضمان توثيق نوع العملية
            b.Property(x => x.Entity).IsRequired().HasMaxLength(50);

            // العمود PrintedAt يُمثل تاريخ ووقت تنفيذ عملية الطباعة
            // تم تحديد نوعه كـ datetime2(3) لتخزين الوقت بدقة أجزاء من الثانية
            b.Property(x => x.PrintedAt).HasColumnType("datetime2(3)");

            // إنشاء فهرس (Index) مركّب على CenterId و Entity و EntityId
            // الهدف: تسريع الاستعلامات عند البحث عن عمليات الطباعة الخاصة بكيان معين داخل مركز محدد
            // هذا الفهرس يُستخدم لتتبع عمليات الطباعة أو إنشاء تقارير حول النشاطات في النظام
            b.HasIndex(x => new { x.CenterId, x.Entity, x.EntityId });
        }
    }
}