// هذا الكلاس يمثل إعدادات تكوين جدول أسعار الإجراءات الطبية لكل مركز (CenterProcedurePrices)
// الغرض منه هو تحديد تكلفة كل إجراء طبي (مثل حشوة، تحليل، عملية...) داخل مركز معين
// لأن كل مركز يمكن أن يحدد أسعارًا مختلفة لنفس الإجراء حسب سياساته
// الجدول يحتوي أيضًا على فترة صلاحية السعر (من تاريخ إلى تاريخ) لدعم تغيير الأسعار بمرور الوقت
// يستخدم هذا الجدول لاحقًا في صفحات الأطباء والفواتير لتحديد السعر الافتراضي للإجراء قبل أن يُسمح للطبيب بتعديله يدويًا

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class CenterProcedurePriceConfig : IEntityTypeConfiguration<CenterProcedurePrice>
    {
        public void Configure(EntityTypeBuilder<CenterProcedurePrice> b)
        {
            // تطبيق الإعدادات العامة مثل المفتاح الأساسي وتواريخ الإنشاء والتعديل والحذف المنطقي
            CommonCfg.Base(b, "CenterProcedurePrices");

            // العمود CurrencyCode لتخزين رمز العملة مثل (USD, SAR, AED)
            // الحد الأقصى للطول 3 حروف حسب معايير ISO 4217
            b.Property(x => x.CurrencyCode).HasMaxLength(3);

            // العمود EffectiveFrom يمثل تاريخ بداية تطبيق السعر
            // يستخدم لتحديد متى يبدأ السعر الجديد بالعمل
            b.Property(x => x.EffectiveFrom).HasColumnType("date");

            // العمود EffectiveTo يمثل تاريخ نهاية صلاحية السعر
            // يُستخدم لإيقاف استخدام السعر بعد فترة معينة أو عند صدور تحديث جديد
            b.Property(x => x.EffectiveTo).HasColumnType("date");

            // إنشاء   (Composite Index) لضمان عدم وجود تكرار لسعر الإجراء نفسه في نفس التاريخ
            // أي أن المركز لا يمكن أن يحدد أكثر من سعر لنفس الإجراء في نفس فترة البداية
            b.HasIndex(x => new { x.CenterId, x.ProcedureId, x.EffectiveFrom }).IsUnique();
        }
    }
}