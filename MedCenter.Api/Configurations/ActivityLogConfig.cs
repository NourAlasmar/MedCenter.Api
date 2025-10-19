using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    // هذا الكلاس يمثل إعدادات (Configuration) خاصة بجدول ActivityLog داخل قاعدة البيانات
    // يتم تطبيقه أوتوماتيكياً في AppDbContext عبر modelBuilder.ApplyConfigurationsFromAssembly(...)
    // هذا الملف يخص سجل النشاطات (Activity Log)، وهو جدول يُستخدم لتتبع كل العمليات التي تتم داخل النظام.

    public class ActivityLogConfig : IEntityTypeConfiguration<ActivityLog>
    {
        public void Configure(EntityTypeBuilder<ActivityLog> b)
        {
            CommonCfg.Base(b, "ActivityLogs");

            // تحديد نوع العمود Timestamp ليكون datetime2 بدقة 3 خانات عشرية (مناسب للتواريخ الدقيقة)
            b.Property(x => x.Timestamp).HasColumnType("datetime2(3)");

            // تحديد أن العمود Action مطلوب (Required) وطوله الأقصى 120 حرفًا
            // يمثل اسم العملية التي تمت (مثلاً: "تعديل فاتورة" أو "حذف مريض")
            b.Property(x => x.Action).IsRequired().HasMaxLength(120);

            // تحديد أن العمود TargetTable يمكن أن يحتوي اسم الجدول المستهدف في العملية (اختياري)
            // مثل: "Invoices" أو "Patients"
            b.Property(x => x.TargetTable).HasMaxLength(80);

            // العمود IPAddress يخزن عنوان الـ IP الذي نفّذ العملية (IPv4 أو IPv6)
            // الحد الأقصى للطول 45 لتغطية جميع الأنواع
            b.Property(x => x.IPAddress).HasMaxLength(45);

            // إنشاء فهرس (Index) على العمود Timestamp لتحسين أداء الاستعلامات حسب التاريخ
            // مع إعطائه اسمًا مخصصًا IX_Activity_Timestamp ليسهل قراءته في قاعدة البيانات
            b.HasIndex(x => x.Timestamp).HasDatabaseName("IX_Activity_Timestamp");
        }
    }
}