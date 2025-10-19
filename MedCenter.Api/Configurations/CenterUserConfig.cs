// هذا الكلاس يمثل إعدادات تكوين جدول المستخدمين المرتبطين بالمراكز الطبية (CenterUsers)
// الغرض منه هو ربط كل مستخدم (من نظام الهوية Identity) بمركز طبي محدد
// لأن المستخدم يمكن أن يكون طبيبًا، محاسبًا، استقبال، أو حتى مستثمرًا في أكثر من مركز
// هذا الجدول هو الوسيط (Join Table) بين المراكز والمستخدمين
// ويحتوي على معلومات إضافية مثل نوع الدور داخل المركز (RoleType) وتاريخ الانضمام (JoinedAt)
// يُستخدم أيضًا للتحكم في صلاحيات المستخدمين وإدارة حالة ارتباطهم بالمركز (IsActive)

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class CenterUserConfig : IEntityTypeConfiguration<CenterUser>
    {
        public void Configure(EntityTypeBuilder<CenterUser> b)
        {
            // تطبيق الإعدادات العامة مثل المفتاح الأساسي، وتواريخ الإنشاء والتعديل، والحذف المنطقي
            CommonCfg.Base(b, "CenterUsers");

            // تحويل نوع الدور (RoleType) من enum إلى byte لتخزينه كرقم صغير في قاعدة البيانات
            // مثل: 1=Admin, 2=Doctor, 3=Reception, 4=Accountant, 5=Investor, 6=Support...
            b.Property(x => x.RoleType).HasConversion<byte>();

            // تحديد نوع العمود JoinedAt ليكون datetime2(3) بدقة أجزاء من الثانية
            // هذا الحقل يمثل تاريخ انضمام المستخدم إلى المركز
            b.Property(x => x.JoinedAt).HasColumnType("datetime2(3)");

            // إنشاء فهرس (Index) لتسريع البحث عن المستخدمين النشطين ضمن مركز محدد
            // الفهرس يجمع بين CenterId و UserId و IsActive
            // مع اسم مخصص UX_CenterUser_Active لسهولة التعرف عليه في قاعدة البيانات
            b.HasIndex(x => new { x.CenterId, x.UserId, x.IsActive })
             .HasDatabaseName("UX_CenterUser_Active");

            // تعريف العلاقة بين CenterUser و Center
            // كل Center يمكن أن يحتوي على عدة Users
            // وعند حذف المركز، يتم حذف جميع المستخدمين المرتبطين به (Cascade Delete)
            b.HasOne(x => x.Center)
             .WithMany(x => x.Users)
             .HasForeignKey(x => x.CenterId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}