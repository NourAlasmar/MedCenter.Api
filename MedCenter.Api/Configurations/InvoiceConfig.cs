// هذا الكلاس يمثل إعدادات تكوين جدول الفواتير (Invoices) في قاعدة البيانات
// الغرض منه هو تخزين جميع فواتير المرضى أو الفواتير الصادرة من المركز للطرف الآخر (مثل التأمين أو الزبون).
// كل سجل في هذا الجدول يمثل فاتورة واحدة تحتوي على رقم الفاتورة، التاريخ، طريقة الدفع، العملة، والمركز التابع له.
// الجدول يُستخدم في القسم المالي لإدارة الفواتير والمدفوعات ومتابعة الحسابات المستحقة.
// كما أنه يرتبط بجدول الفواتير الفرعية (InvoiceItems) لتفاصيل الخدمات أو الإجراءات المدرجة في الفاتورة.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class InvoiceConfig : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> b)
        {
            // تطبيق الإعدادات العامة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "Invoices");

            // العمود InvoiceNo يُمثل رقم الفاتورة (مثلاً INV-2025-001)
            // مطلوب (Required) بطول أقصى 30 حرفًا
            b.Property(x => x.InvoiceNo).IsRequired().HasMaxLength(30);

            // إنشاء فهرس فريد (Unique Index) لضمان عدم تكرار رقم الفاتورة داخل نفس المركز
            // هذا يحافظ على خصوصية ترقيم الفواتير لكل مركز
            b.HasIndex(x => new { x.CenterId, x.InvoiceNo }).IsUnique();

            // العمود CurrencyCode لتخزين رمز العملة المستخدمة في الفاتورة مثل (USD, SAR, AED)
            // الحد الأقصى للطول 3 أحرف حسب معيار ISO4217
            b.Property(x => x.CurrencyCode).HasMaxLength(3);

            // العمود PaymentMethod لتخزين طريقة الدفع (نقدي، بطاقة، تحويل...)
            // يتم تحويله من enum إلى byte لتخزينه كقيمة رقمية صغيرة
            b.Property(x => x.PaymentMethod).HasConversion<byte>();

            // العمود InvoiceDate لتخزين تاريخ إصدار الفاتورة بدقة أجزاء من الثانية
            b.Property(x => x.InvoiceDate).HasColumnType("datetime2(3)");

            // إنشاء فهرس إضافي لتسريع عمليات البحث عن الفواتير حسب المركز وتاريخ الفاتورة
            // مفيد جدًا في تقارير الفواتير اليومية أو الشهرية
            b.HasIndex(x => new { x.CenterId, x.InvoiceDate });
        }
    }
}