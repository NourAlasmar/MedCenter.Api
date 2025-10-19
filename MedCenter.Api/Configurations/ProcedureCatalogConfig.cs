// هذا الكلاس يمثل إعدادات تكوين جدول دليل الإجراءات الطبية (ProcedureCatalog) في قاعدة البيانات
// الغرض منه هو إنشاء قائمة شاملة بالإجراءات أو الخدمات الطبية التي يمكن تنفيذها في النظام.
// مثل (حشوة سن، عملية قسطرة، فحص نظر، علاج جلدي... إلخ).
// كل سجل في هذا الجدول يمثل إجراءً طبيًا واحدًا مرتبطًا بتخصص معين (Specialty).
// الجدول يُستخدم كأساس في صفحات الطبيب، الفواتير، المواعيد، والتقارير لتحديد نوع الخدمة المنفذة وسعرها.
// كما يمكن للمسؤول (Admin) أو صاحب المركز تعديل أو إضافة إجراءات جديدة حسب طبيعة العيادة.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class ProcedureCatalogConfig : IEntityTypeConfiguration<ProcedureCatalog>
    {
        public void Configure(EntityTypeBuilder<ProcedureCatalog> b)
        {
            // تطبيق الإعدادات العامة الموحدة (المفتاح الأساسي، الحذف المنطقي، تواريخ الإنشاء والتعديل)
            CommonCfg.Base(b, "ProcedureCatalog");

            // العمود Name يُمثل اسم الإجراء (مثل: "تنظيف أسنان"، "تحليل دم شامل")
            // مطلوب (Required) بطول أقصى 200 حرف لضمان وضوح الاسم وتفادي التكرار
            b.Property(x => x.Name).IsRequired().HasMaxLength(200);

            // العمود Code يُمثل كود الإجراء الطبي (مثل رمز CPT أو كود داخلي خاص بالنظام)
            // اختياري (Optional) بطول أقصى 50 حرفًا — لتسهيل عمليات الفوترة والتقارير الدولية أو المحلية
            b.Property(x => x.Code).HasMaxLength(50);

            // إنشاء فهرس (Index) فريد يجمع بين SpecialtyId و Name
            // الهدف: منع تكرار نفس الإجراء داخل نفس التخصص الطبي
            // أي أنه يمكن أن يكون هناك "تنظيف أسنان" في تخصص الأسنان، و"تنظيف بشرة" في تخصص الجلدية، بدون تعارض
            b.HasIndex(x => new { x.SpecialtyId, x.Name }).IsUnique();
        }
    }
}