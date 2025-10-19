// هذا الكلاس يمثل إعدادات تكوين جدول دفعات المخزون (InventoryBatches) في قاعدة البيانات
// الغرض منه هو إدارة وتتبع الدفعات (Batches) الخاصة بالأدوية أو المستلزمات الطبية داخل المخزون.
// كل دفعة تحتوي على رقم تسلسلي (BatchNo)، تاريخ انتهاء صلاحية، وكمية محددة.
// هذا الجدول يساعد في نظام إدارة المخزون على مراقبة صلاحيات المنتجات وتنبيه المستخدم قبل انتهاء صلاحيتها.
// كما يُستخدم أيضًا لحساب التكلفة بناءً على طريقة الإدخال (FIFO أو LIFO).

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class InventoryBatchConfig : IEntityTypeConfiguration<InventoryBatch>
    {
        public void Configure(EntityTypeBuilder<InventoryBatch> b)
        {
            // تطبيق الإعدادات العامة الموحدة (المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل)
            CommonCfg.Base(b, "InventoryBatches");

            // العمود BatchNo يُمثل رقم الدفعة الخاصة بالمنتج
            // يستخدم عادة لتتبع مصدر الدفعة أو تاريخ تصنيعها
            // الحد الأقصى للطول 50 حرفًا (لتغطية جميع أنواع الأكواد)
            b.Property(x => x.BatchNo).HasMaxLength(50);

            // العمود ExpiryDate يُمثل تاريخ انتهاء صلاحية الدفعة
            // نوع العمود هو date لأننا نحتاج التاريخ فقط بدون وقت
            b.Property(x => x.ExpiryDate).HasColumnType("date");

            // إنشاء فهرس (Index) على ItemId
            // الهدف منه تسريع الاستعلامات التي تبحث عن جميع الدفعات التابعة لمنتج معين
            b.HasIndex(x => x.ItemId);
        }
    }
}