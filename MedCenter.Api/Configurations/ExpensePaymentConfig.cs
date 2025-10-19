// هذا الكلاس يمثل إعدادات تكوين جدول دفعات المصاريف (ExpensePayments) في قاعدة البيانات
// الغرض منه هو تخزين تفاصيل الدفعات التي تم سدادها مقابل المصاريف المسجلة في جدول (Expenses).
// كل سجل في هذا الجدول يمثل عملية دفع واحدة سواء كانت دفعة كاملة أو جزئية.
// من خلال هذا الجدول يمكن معرفة تاريخ الدفع، المبلغ المدفوع، وطريقة الدفع.
// يُستخدم في القسم المالي للمركز لتتبع السداد، استخراج التقارير، وحساب المصاريف المتبقية بدقة.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class ExpensePaymentConfig : IEntityTypeConfiguration<ExpensePayment>
    {
        public void Configure(EntityTypeBuilder<ExpensePayment> b)
        {
            // تطبيق الإعدادات العامة المشتركة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "ExpensePayments");

            // العمود Method يُمثل طريقة الدفع المستخدمة (نقدي، تحويل بنكي، شيك، إلخ)
            // يتم تحويلها من enum إلى byte لتخزينها كقيمة رقمية صغيرة في قاعدة البيانات
            b.Property(x => x.Method).HasConversion<byte>();

            // العمود PaymentDate يُمثل التاريخ والوقت الفعلي للدفع
            // تم تحديد نوعه كـ datetime2(3) لتخزين الوقت بدقة أجزاء من الثانية
            b.Property(x => x.PaymentDate).HasColumnType("datetime2(3)");

            // إنشاء فهرس (Index) على العمود ExpenseId
            // الهدف منه تسريع عمليات البحث عن جميع الدفعات المرتبطة بمصروف معين
            b.HasIndex(x => x.ExpenseId);
        }
    }
}