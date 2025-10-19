// هذا الكلاس يمثل إعدادات تكوين جدول قائمة التشخيصات (DiagnosisCatalog) في قاعدة البيانات
// الغرض منه هو حفظ جميع أنواع التشخيصات الطبية التي يمكن للطبيب استخدامها في النظام
// مثل: "تسوس الأسنان"، "التهاب اللثة"، "حب الشباب"، "ارتفاع الضغط"، وغيرها.
// يمكن أن تكون التشخيصات عامة (تابعة لتخصص محدد فقط) أو خاصة بمركز معين (CenterId)
// هذا الجدول يُستخدم في صفحة الطبيب لإضافة أو اختيار تشخيص للمريض بسهولة من قائمة جاهزة
// كما يُستخدم من قبل الأدمن لإدارة وإضافة تشخيصات جديدة لأنواع العيادات المختلفة.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class DiagnosisCatalogConfig : IEntityTypeConfiguration<DiagnosisCatalog>
    {
        public void Configure(EntityTypeBuilder<DiagnosisCatalog> b)
        {
            // تطبيق الإعدادات العامة الأساسية مثل المفتاح الأساسي وتواريخ الإنشاء والتعديل والحذف المنطقي
            CommonCfg.Base(b, "DiagnosisCatalog");

            // العمود Name يمثل اسم التشخيص الطبي (مثل "تسوس الأسنان" أو "التهاب الجلد")
            // تم تعيينه كمطلوب (Required) بطول أقصى 200 حرف
            b.Property(x => x.Name).IsRequired().HasMaxLength(200);

            // إنشاء فهرس مركب (Composite Index) لضمان عدم تكرار نفس التشخيص ضمن نفس التخصص والمركز
            // أي لا يمكن أن يكون لدينا تشخيص بنفس الاسم مكررًا لتخصص معين في نفس المركز
            // الفهرس مكوّن من: SpecialtyId + Name + CenterId
            b.HasIndex(x => new { x.SpecialtyId, x.Name, x.CenterId }).IsUnique();
        }
    }
}