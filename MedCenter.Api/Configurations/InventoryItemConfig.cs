// هذا الكلاس يمثل إعدادات تكوين جدول عناصر المخزون (InventoryItems) في قاعدة البيانات
// الغرض منه هو تعريف كل مادة أو منتج موجود في المخزون داخل المركز الطبي.
// مثل: أدوات طبية، مستلزمات، أدوية، أو مواد استهلاكية.
// هذا الجدول يُعتبر الأساس لنظام إدارة المخزون، حيث يتم ربطه بدُفعات المخزون (InventoryBatches)
// وحركات الإدخال والإخراج (InventoryMovements) لتتبع الكميات المتوفرة والمستهلكة في الوقت الفعلي.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class InventoryItemConfig : IEntityTypeConfiguration<InventoryItem>
    {
        public void Configure(EntityTypeBuilder<InventoryItem> b)
        {
            // تطبيق الإعدادات العامة المشتركة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "InventoryItems");

            // العمود Name يُخزن اسم العنصر (مثل "قفازات طبية" أو "دواء باراسيتامول")
            // تم تعيينه كمطلوب (Required) بطول أقصى 200 حرف لضمان وضوح الاسم وتفادي التكرار
            b.Property(x => x.Name).IsRequired().HasMaxLength(200);

            // العمود SKU يُخزن رقم تعريف العنصر (Stock Keeping Unit)
            // يُستخدم لتتبع المنتجات بسهولة داخل النظام
            // الحد الأقصى للطول 50 حرفًا (لتغطية الأكواد الطويلة أو الرموز المخصصة)
            b.Property(x => x.SKU).HasMaxLength(50);

            // العمود Unit يُحدد وحدة القياس (مثل "علبة"، "قطعة"، "كرتونة"، "مل")
            // مطلوب (Required) بطول أقصى 20 حرفًا
            b.Property(x => x.Unit).IsRequired().HasMaxLength(20);

            // إنشاء فهرس (Index) يجمع بين CenterId و Name
            // الهدف منه ضمان عدم وجود عنصر بنفس الاسم داخل نفس المركز أكثر من مرة
            // الفهرس فريد (Unique) لمنع التكرار وتحسين أداء البحث
            b.HasIndex(x => new { x.CenterId, x.Name }).IsUnique();
        }
    }
}