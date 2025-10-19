// هذا الكلاس يمثل إعدادات تكوين جدول فواتير المركز (CenterBills) في قاعدة البيانات
// الهدف منه هو تخزين جميع الفواتير الخاصة بالمركز الطبي نفسه، مثل:
// فواتير الإيجار، الكهرباء، الأجهزة، الإنترنت، أو أقساط مشتريات.
// الجدول يدعم أنواع متعددة من الفواتير (مرة واحدة أو متكررة شهريًا)
// ويُستخدم لاحقًا في الحسابات المالية للمركز لاحتساب المصروفات الشهرية والتزامات المركز.
// لا يتعلق هذا الجدول بمرضى أو أطباء، بل بمصاريف تشغيل المركز نفسه.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class CenterBillConfig : IEntityTypeConfiguration<CenterBill>
    {
        public void Configure(EntityTypeBuilder<CenterBill> b)
        {
            CommonCfg.Base(b, "CenterBills");

            // العمود Vendor يُستخدم لتخزين اسم الجهة المصدّرة للفاتورة (مثل شركة الكهرباء أو المورد)
            // مطلوب (Required) وطوله الأقصى 150 حرفًا
            b.Property(x => x.Vendor).IsRequired().HasMaxLength(150);

            // العمود BillType يحدد نوع الفاتورة (مرة واحدة / متكررة / أقساط)
            // يتم تخزينه كـ tinyint (رقم صغير لتوفير المساحة)
            b.Property(x => x.BillType).HasColumnType("tinyint");

            // تاريخ بداية الفاتورة (StartDate) يستخدم عند وجود اشتراكات أو فواتير متكررة
            // تم تحديد نوع العمود كـ date (تاريخ فقط دون وقت)
            b.Property(x => x.StartDate).HasColumnType("date");

            // تاريخ نهاية الفاتورة (EndDate) عند انتهاء الفترة أو سداد آخر قسط
            b.Property(x => x.EndDate).HasColumnType("date");
        }
    }
}