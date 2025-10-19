// هذا الكلاس يمثل إعدادات تكوين جدول طلبات المخابر (LabOrders) في قاعدة البيانات
// الغرض منه هو تخزين الطلبات التي يرسلها الأطباء إلى المخابر لإجراء فحوصات أو تحاليل أو أعمال مخبرية.
// كل سجل يمثل طلب مخبري واحد يحتوي على تاريخ الطلب، الحالة (قيد التنفيذ، منجز، ملغى...)، والمركز التابع له.
// الجدول يُستخدم في صفحات الطبيب والمخبر والإدارة لمتابعة حالة الطلبات وتسديد المستحقات المالية للمخبر.
// كما يُسهم في إنشاء تقارير عن عدد الفحوصات المنفذة، قيمتها، وأدائها عبر الزمن.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class LabOrderConfig : IEntityTypeConfiguration<LabOrder>
    {
        public void Configure(EntityTypeBuilder<LabOrder> b)
        {
            // تطبيق الإعدادات العامة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "LabOrders");

            // العمود OrderDate يُمثل تاريخ ووقت إنشاء الطلب المخبري
            // تم تحديد نوعه كـ datetime2(3) لتخزين الوقت بدقة أجزاء من الثانية
            b.Property(x => x.OrderDate).HasColumnType("datetime2(3)");

            // إنشاء فهرس (Index) مركّب على CenterId و Status و OrderDate
            // الهدف: تسريع الاستعلامات الخاصة بجلب الطلبات لمركز معين بحسب حالتها وتاريخها
            // مفيد جدًا في تقارير التحاليل والإجراءات اليومية للمخبر أو الطبيب
            b.HasIndex(x => new { x.CenterId, x.Status, x.OrderDate });
        }
    }
}