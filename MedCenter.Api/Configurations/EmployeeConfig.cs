// هذا الكلاس يمثل إعدادات تكوين جدول الموظفين (Employees) في قاعدة البيانات
// الغرض منه هو تخزين جميع بيانات الموظفين العاملين في المركز الطبي، سواء كانوا أطباء أو إداريين أو فنيين.
// يحتوي الجدول على معلومات أساسية مثل الاسم، المسمى الوظيفي، تاريخ التعيين، وحالة التفعيل.
// هذا الجدول يُستخدم في قسم الموارد البشرية (HR) لإدارة بيانات الموظفين، الرواتب، العقود، والحضور.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> b)
        {
            // تطبيق الإعدادات العامة الموحدة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "Employees");

            // العمود FullName يمثل الاسم الكامل للموظف
            // مطلوب (Required) لضمان أن كل موظف له اسم واضح، بطول أقصى 200 حرف
            b.Property(x => x.FullName).IsRequired().HasMaxLength(200);

            // العمود JobTitle يمثل المسمى الوظيفي (مثل طبيب أسنان، ممرضة، محاسب...)
            // مطلوب أيضًا بطول أقصى 150 حرف
            b.Property(x => x.JobTitle).IsRequired().HasMaxLength(150);

            // العمود HireDate لتخزين تاريخ تعيين الموظف في المركز
            // تم تحديد نوعه كـ date (يخزن التاريخ فقط بدون الوقت)
            b.Property(x => x.HireDate).HasColumnType("date");

            // إنشاء فهرس (Index) على CenterId و IsActive
            // الهدف من هذا الفهرس هو تسريع عمليات البحث عن الموظفين النشطين داخل مركز معين
            b.HasIndex(x => new { x.CenterId, x.IsActive });
        }
    }
}