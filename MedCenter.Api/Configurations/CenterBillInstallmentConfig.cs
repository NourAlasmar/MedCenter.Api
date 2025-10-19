// هذا الكلاس يمثل إعدادات تكوين جدول أقساط فواتير المركز (CenterBillInstallments) في قاعدة البيانات
// الغرض منه هو ربط الفواتير (CenterBills) بأقساط الدفع الخاصة بها
// مثلاً: فاتورة شراء جهاز طبي يمكن تقسيمها إلى عدة دفعات (أقساط) تُسجل هنا
// كل سجل يمثل دفعة واحدة مع تاريخ استحقاقها وطريقة الدفع وحالتها (مدفوعة / غير مدفوعة).
// يساعد هذا الجدول الإدارة المالية للمركز على تتبع الأقساط المستقبلية والمبالغ المستحقة بدقة.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class CenterBillInstallmentConfig : IEntityTypeConfiguration<CenterBillInstallment>
    {
        public void Configure(EntityTypeBuilder<CenterBillInstallment> b)
        {
            CommonCfg.Base(b, "CenterBillInstallments");

            // تحديد نوع العمود DueDate ليكون date فقط (تاريخ بدون وقت)
            // هذا الحقل يمثل تاريخ استحقاق القسط المحدد
            b.Property(x => x.DueDate).HasColumnType("date");

            // تحويل نوع العمود PaymentMethod من enum إلى byte? (قابل للقيمة الفارغة)
            // لتخزين طريقة الدفع بشكل رقمي صغير مثل: 1=نقدي، 2=تحويل، 3=شيك
            b.Property(x => x.PaymentMethod).HasConversion<byte?>();

            // تحويل حالة القسط (Status) من enum إلى byte لتخزينها كرقم صغير
            // مثل: 0=قيد الانتظار، 1=مدفوع، 2=متأخر
            b.Property(x => x.Status).HasConversion<byte>();

            // إنشاء  (Index) على CenterBillId لتحسين أداء الاستعلامات المتعلقة بالفواتير
            // الهدف تسريع البحث عن جميع الأقساط التابعة لفاتورة معينة
            b.HasIndex(x => x.CenterBillId);
        }
    }
}