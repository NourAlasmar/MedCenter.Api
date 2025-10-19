// هذا الكلاس يمثل إعدادات تكوين جدول المراكز الطبية (Centers) في قاعدة البيانات
// الغرض منه هو تعريف خصائص كل مركز طبي مسجّل في النظام
// كل مركز يمكن أن يمتلك أطباء، موظفين، مرضى، ومستخدمين مرتبطين به
// يحتوي الجدول على معلومات مثل الاسم، البريد الإلكتروني، الهاتف، نوع الاشتراك، والشعارات أو القوالب الخاصة به
// هذه البيانات تعتبر الأساس الذي تُبنى عليه بقية أقسام النظام (Appointments, Finance, Users...)
// أي أن كل العمليات في النظام تكون مرتبطة بمركز محدد (CenterId)

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class CenterConfig : IEntityTypeConfiguration<Center>
    {
        public void Configure(EntityTypeBuilder<Center> b)
        {
            CommonCfg.Base(b, "Centers");

            // العمود Name يمثل اسم المركز الطبي
            // مطلوب (Required) ولا يمكن أن يكون مكررًا (Unique) لضمان عدم تكرار أسماء المراكز
            b.Property(x => x.Name).IsRequired().HasMaxLength(200);
            b.HasIndex(x => x.Name).IsUnique();

            // العمود Email لتخزين البريد الإلكتروني الرسمي للمركز (اختياري)
            // الحد الأقصى للطول 150 حرفًا
            b.Property(x => x.Email).HasMaxLength(150);

            // العمود Phone لتخزين رقم الاتصال بالمركز (اختياري)
            // الحد الأقصى للطول 30 حرفًا لتغطية جميع الصيغ الممكنة (بما فيها رمز الدولة)
            b.Property(x => x.Phone).HasMaxLength(30);

            // العمود SubscriptionType يحدد نوع الاشتراك للمركز (شهري، سنوي، تجريبي...)
            // يتم تحويله من enum إلى byte للتخزين كرقم صغير
            b.Property(x => x.SubscriptionType).HasConversion<byte>();

            // العمود InvoiceTemplateUrl لتخزين رابط قالب الفواتير الخاص بالمركز
            // يمكن أن يكون هذا الرابط يشير إلى ملف تصميم أو JSON خاص بالهوية البصرية
            b.Property(x => x.InvoiceTemplateUrl).HasMaxLength(300);

            // العمود LogoUrl لتخزين رابط شعار المركز (Logo)
            b.Property(x => x.LogoUrl).HasMaxLength(300);
        }
    }
}