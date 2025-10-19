// هذا الكلاس يمثل إعدادات تكوين جدول الحضور (Attendance) في قاعدة البيانات
// الهدف منه هو تحديد كيفية تخزين بيانات حضور الموظفين (الأطباء أو الإداريين)
// كل سجل في هذا الجدول يمثل حضور موظف في يوم محدد، مع إمكانية تتبع حالة الحضور (حضر / غاب / تأخر...)
// يساعد هذا الجدول في نظام الموارد البشرية (HR) لتوليد تقارير الحضور، الغياب، وساعات العمل الشهرية

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class AttendanceConfig : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> b)
        {
            CommonCfg.Base(b, "Attendance");

            // تحديد نوع العمود Day ليكون من نوع date (تاريخ فقط بدون وقت)
            // هذا العمود يمثل تاريخ يوم الحضور
            b.Property(x => x.Day).HasColumnType("date");

            // إنشاء  (Index) مركب يجمع بين EmployeeId و Day
            // مع تحديد أنه فريد (IsUnique) حتى لا يُسجّل نفس الموظف مرتين في نفس اليوم
            // هذا يمنع التكرار في بيانات الحضور ويضمن الاتساق
            b.HasIndex(x => new { x.EmployeeId, x.Day }).IsUnique();
        }
    }
}