// هذا الكلاس يمثل إعدادات تكوين جدول المدفوعات (Payments) في قاعدة البيانات
// الغرض منه هو تخزين جميع عمليات الدفع التي تتم من قبل المرضى أو العملاء مقابل الفواتير.
// كل سجل في هذا الجدول يمثل دفعة واحدة سواء كانت جزئية أو كاملة.
// الجدول يُستخدم في القسم المالي والمحاسبة لربط الدفعات بالفواتير (Invoices) وتتبع الحالة المالية للمريض أو المركز.
// كما يساعد في إعداد التقارير الشهرية والإحصائيات الخاصة بالإيرادات وطرق الدفع المختلفة.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class PaymentConfig : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> b)
        {
            // تطبيق الإعدادات العامة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "Payments");

            // العمود Method يُحدد طريقة الدفع (مثل: نقدي، بطاقة، تحويل بنكي، إلكتروني)
            // يتم تحويله من enum إلى byte لتخزينه كقيمة رقمية صغيرة في قاعدة البيانات
            b.Property(x => x.Method).HasConversion<byte>();

            // العمود PaymentDate يُخزن التاريخ والوقت الفعلي للدفع
            // تم تحديد نوعه كـ datetime2(3) لتخزين الوقت بدقة عالية (حتى أجزاء من الثانية)
            b.Property(x => x.PaymentDate).HasColumnType("datetime2(3)");

            // إنشاء فهرس (Index) مركّب على InvoiceId و PaymentDate
            // الهدف: تسريع الاستعلامات الخاصة بجلب المدفوعات المرتبطة بفاتورة معينة مرتبة حسب التاريخ
            // مفيد في صفحات الفواتير والتقارير المالية
            b.HasIndex(x => new { x.InvoiceId, x.PaymentDate });
        }
    }
}