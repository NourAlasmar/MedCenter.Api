// هذا الكلاس يمثل إعدادات تكوين جدول عقود الموظفين (EmployeeContracts) في قاعدة البيانات
// الغرض منه هو تخزين بيانات العقود الخاصة بالموظفين داخل كل مركز طبي.
// كل سجل يمثل عقد عمل واحد لموظف محدد ويحتوي على نوع العقد، مدته، حالته، ورابط المستند.
// هذا الجدول يُستخدم في قسم الموارد البشرية (HR) لمتابعة تجديد العقود، إنهائها، أو توثيقها.
// كما يُستخدم لاحقًا في حسابات الرواتب لتحديد فترة العمل الفعلية أو المكافآت بناءً على العقد.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class EmployeeContractConfig : IEntityTypeConfiguration<EmployeeContract>
    {
        public void Configure(EntityTypeBuilder<EmployeeContract> b)
        {
            // تطبيق الإعدادات العامة (المفتاح الأساسي + الحذف المنطقي + تواريخ الإنشاء والتعديل)
            CommonCfg.Base(b, "EmployeeContracts");

            // العمود ContractType لتخزين نوع العقد (دوام كامل، جزئي، مؤقت...)
            // مطلوب (Required) بطول أقصى 80 حرفًا
            b.Property(x => x.ContractType).IsRequired().HasMaxLength(80);

            // العمود StartDate لتخزين تاريخ بداية العقد
            // يُستخدم لمعرفة مدة الخدمة واحتساب المستحقات لاحقًا
            b.Property(x => x.StartDate).HasColumnType("date");

            // العمود EndDate لتخزين تاريخ نهاية العقد
            // في حال كان العقد محدد المدة أو يحتاج إلى تجديد دوري
            b.Property(x => x.EndDate).HasColumnType("date");

            // العمود Status لتخزين حالة العقد مثل:
            // 0 = نشط، 1 = منتهي، 2 = مجدد، 3 = موقوف
            // يتم تخزينه كـ tinyint (رقم صغير لتوفير المساحة)
            b.Property(x => x.Status).HasColumnType("tinyint");

            // العمود DocumentUrl لتخزين رابط ملف العقد المرفوع (PDF مثلاً)
            // الحد الأقصى للطول 500 حرف (كافٍ لروابط السحابة أو السيرفر المحلي)
            b.Property(x => x.DocumentUrl).HasMaxLength(500);
        }
    }
}