// هذا الكلاس يمثل إعدادات تكوين جدول تقييمات الموظفين (EmployeeRatings) في قاعدة البيانات
// الغرض منه هو تخزين تقييمات الأداء التي يحصل عليها الموظفون بشكل دوري (شهري، ربع سنوي...).
// كل سجل في هذا الجدول يمثل عملية تقييم واحدة لموظف معين، تحتوي على التاريخ، النتيجة (Score)، والملاحظات.
// يُستخدم هذا الجدول في قسم الموارد البشرية (HR) لمتابعة أداء الموظفين ومكافآتهم وتحسين جودة العمل.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class EmployeeRatingConfig : IEntityTypeConfiguration<EmployeeRating>
    {
        public void Configure(EntityTypeBuilder<EmployeeRating> b)
        {
            // تطبيق الإعدادات العامة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "EmployeeRatings");

            // العمود RatedAt يُخزن تاريخ التقييم الفعلي
            // تم تحديد نوعه كـ datetime2(3) لتسجيل الوقت بدقة أجزاء من الثانية
            b.Property(x => x.RatedAt).HasColumnType("datetime2(3)");

            // العمود Score يُخزن نتيجة التقييم (عادة من 1 إلى 5 أو من 0 إلى 10)
            // يتم تحويله من enum أو قيمة رقمية إلى byte لتوفير المساحة
            b.Property(x => x.Score).HasConversion<byte>();

            // إنشاء فهرس (Index) على EmployeeId
            // الهدف: تسريع عمليات البحث عند جلب جميع التقييمات الخاصة بموظف معين
            b.HasIndex(x => x.EmployeeId);
        }
    }
}