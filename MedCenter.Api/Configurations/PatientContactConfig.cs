// هذا الكلاس يمثل إعدادات تكوين جدول جهات الاتصال الخاصة بالمرضى (PatientContacts) في قاعدة البيانات
// الغرض منه هو تخزين بيانات الأشخاص الذين يمكن التواصل معهم في حال الطوارئ أو عند الحاجة.
// كل سجل يمثل جهة اتصال مرتبطة بمريض معين (مثل قريب، ولي أمر، أو شخص معتمد للتواصل).
// يُستخدم هذا الجدول في قسم الاستقبال أو الإدارة الطبية لتحديث بيانات الطوارئ وضمان سرعة التواصل عند الحاجة.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class PatientContactConfig : IEntityTypeConfiguration<PatientContact>
    {
        public void Configure(EntityTypeBuilder<PatientContact> b)
        {
            // تطبيق الإعدادات العامة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "PatientContacts");

            // العمود Name يُمثل اسم جهة الاتصال (مثل: "الأب"، "الزوجة"، "صديق")
            // مطلوب (Required) بطول أقصى 150 حرف
            b.Property(x => x.Name).IsRequired().HasMaxLength(150);

            // العمود Phone يُمثل رقم الهاتف الخاص بجهة الاتصال
            // اختياري (قد لا يكون متاحًا دائمًا)، بطول أقصى 30 حرفًا لتغطية الأرقام المحلية والدولية
            b.Property(x => x.Phone).HasMaxLength(30);

            // إنشاء فهرس (Index) على PatientId
            // الهدف: تسريع عمليات البحث عن جميع جهات الاتصال الخاصة بمريض معين
            b.HasIndex(x => x.PatientId);
        }
    }
}