// هذا الكلاس يمثل إعدادات تكوين جدول حركات المخزون (InventoryMovements) في قاعدة البيانات
// الغرض منه هو تسجيل جميع العمليات التي تتم على عناصر المخزون (إضافة، إخراج، تعديل كمية...)
// كل سجل في هذا الجدول يمثل عملية واحدة تمت على عنصر معين مثل استلام دفعة، استخدام مواد، أو إرجاعها.
// هذا الجدول يُعتبر محورياً لتتبع كميات المخزون والتأكد من توازن الرصيد الحالي لكل منتج داخل كل مركز طبي.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class InventoryMovementConfig : IEntityTypeConfiguration<InventoryMovement>
    {
        public void Configure(EntityTypeBuilder<InventoryMovement> b)
        {
            // تطبيق الإعدادات العامة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "InventoryMovements");

            // العمود MovementType يُحدد نوع الحركة في المخزون (إدخال، إخراج، تعديل... إلخ)
            // يتم تحويله من enum إلى byte لتخزينه كقيمة رقمية صغيرة داخل قاعدة البيانات
            b.Property(x => x.MovementType).HasConversion<byte>();

            // العمود MovedAt يُمثل تاريخ ووقت تنفيذ الحركة الفعلية في النظام
            // تم تحديد نوعه كـ datetime2(3) لتخزين الوقت بدقة أجزاء من الثانية
            b.Property(x => x.MovedAt).HasColumnType("datetime2(3)");

            // إنشاء فهرس (Index) مركّب لتسريع عمليات البحث في الحركات الخاصة بعنصر معين داخل مركز معين
            // الفهرس يجمع بين CenterId و ItemId و MovedAt
            // هذا يسمح بجلب جميع الحركات لعناصر معينة ضمن فترة زمنية بسرعة
            b.HasIndex(x => new { x.CenterId, x.ItemId, x.MovedAt });
        }
    }
}