// هذا الكلاس يمثل إعدادات تكوين جدول المرضى (Patients) في قاعدة البيانات
// الغرض منه هو تخزين البيانات الأساسية للمرضى المسجلين في النظام داخل كل مركز طبي.
// يحتوي الجدول على معلومات مثل الاسم الكامل، رقم الهاتف، والمركز التابع له.
// هذا الجدول هو أحد أهم الجداول في النظام، إذ يُستخدم في كل من صفحات الأطباء، الاستقبال، المحاسبة، والتقارير.
// يتم من خلاله ربط جميع السجلات الطبية، الفواتير، المواعيد، والتشخيصات الخاصة بكل مريض.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class PatientConfig : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> b)
        {
            // تطبيق الإعدادات العامة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "Patients");

            // العمود FullName يُمثل الاسم الكامل للمريض (مثلاً: "أحمد خالد العلي")
            // مطلوب (Required) بطول أقصى 200 حرف لضمان الاتساق
            b.Property(x => x.FullName).IsRequired().HasMaxLength(200);

            // العمود Phone يُخزن رقم الاتصال بالمريض (اختياري)
            // الحد الأقصى للطول 30 حرفًا لتغطية صيغ الأرقام المحلية والدولية
            b.Property(x => x.Phone).HasMaxLength(30);

            // إنشاء فهرس (Index) يجمع بين CenterId و FullName
            // الهدف: تسريع عمليات البحث عن المريض داخل مركز معين بالاسم
            // كما يساعد في منع التكرار غير المقصود لأسماء المرضى داخل نفس المركز
            b.HasIndex(x => new { x.CenterId, x.FullName });
        }
    }
}