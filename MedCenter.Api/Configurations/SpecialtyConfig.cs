// هذا الكلاس يمثل إعدادات تكوين جدول التخصصات الطبية (Specialties) في قاعدة البيانات
// الغرض منه هو تعريف التخصصات الطبية المختلفة داخل النظام مثل (طب الأسنان، الجلدية، التجميل، العيون، الباطنية...).
// يُعتبر هذا الجدول مرجعًا (Reference Table) يُستخدم لربط الأطباء، الإجراءات، والتشخيصات بتخصص محدد.
// يساعد النظام على تصنيف الخدمات الطبية بشكل منظم، وتخصيص واجهات أو أسعار معينة لكل تخصص.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class SpecialtyConfig : IEntityTypeConfiguration<Specialty>
    {
        public void Configure(EntityTypeBuilder<Specialty> b)
        {
            // تطبيق الإعدادات العامة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "Specialties");

            // العمود Name يُمثل اسم التخصص الطبي (مثل "طب الأسنان" أو "الأمراض الجلدية")
            // مطلوب (Required) بطول أقصى 100 حرف لضمان الوضوح والتمييز بين التخصصات
            b.Property(x => x.Name).IsRequired().HasMaxLength(100);

            // إنشاء فهرس (Index) فريد على العمود Name
            // الهدف: منع تكرار نفس التخصص في قاعدة البيانات
            // لضمان أن كل تخصص له سجل واحد فقط داخل النظام
            b.HasIndex(x => x.Name).IsUnique();
        }
    }
}