// هذا الكلاس يمثل إعدادات تكوين جدول المخابر (Labs) في قاعدة البيانات
// الغرض منه هو تخزين معلومات المخابر الطبية المتعاقدة أو التابعة للمركز الطبي.
// كل سجل في هذا الجدول يمثل مخبرًا واحدًا يحتوي على بيانات مثل الاسم، رقم الهاتف، والمركز المرتبط به.
// هذا الجدول يُستخدم في النظام لربط الإجراءات المخبرية (مثل التحاليل الطبية أو التركيبات السنية) بالمخبر المنفذ لها.
// كما يُتيح للمركز تتبع الحسابات المالية للمخبر (المبالغ المستحقة والمدفوعة) وإدارة التعاون بين الطرفين.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class LabConfig : IEntityTypeConfiguration<Lab>
    {
        public void Configure(EntityTypeBuilder<Lab> b)
        {
            // تطبيق الإعدادات العامة الأساسية مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "Labs");

            // العمود Name يُمثل اسم المخبر (مثل "مختبر الحياة الطبية")
            // مطلوب (Required) لضمان وجود اسم لكل سجل، بطول أقصى 200 حرف
            b.Property(x => x.Name).IsRequired().HasMaxLength(200);

            // العمود Phone يُمثل رقم الاتصال بالمخبر
            // اختياري، بطول أقصى 30 حرفًا لتغطية جميع صيغ الأرقام مع رموز الدول
            b.Property(x => x.Phone).HasMaxLength(30);

            // إنشاء فهرس (Index) على CenterId
            // الهدف منه تسريع عمليات البحث عن جميع المخابر التابعة لمركز معين
            b.HasIndex(x => x.CenterId);
        }
    }
}