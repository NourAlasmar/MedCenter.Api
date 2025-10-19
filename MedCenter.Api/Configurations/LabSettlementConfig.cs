// هذا الكلاس يمثل إعدادات تكوين جدول التسويات المالية مع المخابر (LabSettlements) في قاعدة البيانات
// الغرض منه هو تسجيل التسويات المالية بين المراكز الطبية والمخابر المتعاقدة.
// كل سجل في هذا الجدول يمثل تسوية لفترة محددة، تتضمن حساب الأعمال المنجزة، المبالغ المستحقة والمدفوعة، وصافي الرصيد.
// يُستخدم هذا الجدول في النظام المالي للمركز لتتبع المستحقات الشهرية أو الدورية للمخابر، وضمان دقة السجلات المالية.
// كما يمكن من خلاله إنشاء تقارير مالية تفصيلية بين المركز والمخبر لفترات زمنية معينة.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class LabSettlementConfig : IEntityTypeConfiguration<LabSettlement>
    {
        public void Configure(EntityTypeBuilder<LabSettlement> b)
        {
            // تطبيق الإعدادات العامة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "LabSettlements");

            // العمود PeriodStart يُمثل تاريخ بداية فترة التسوية (مثلاً بداية الشهر)
            // تم تحديد نوعه كـ date لتخزين التاريخ فقط
            b.Property(x => x.PeriodStart).HasColumnType("date");

            // العمود PeriodEnd يُمثل تاريخ نهاية فترة التسوية (مثلاً نهاية الشهر)
            // يُستخدم مع PeriodStart لتحديد الفترة المالية المغطاة
            b.Property(x => x.PeriodEnd).HasColumnType("date");

            // إنشاء فهرس (Index) مركّب يجمع بين CenterId و LabId و PeriodStart و PeriodEnd
            // الهدف: منع تكرار التسويات لنفس المخبر ونفس الفترة داخل نفس المركز
            // أي أنه لا يمكن تسجيل تسوية مالية لنفس المخبر مرتين في نفس المدة
            b.HasIndex(x => new { x.CenterId, x.LabId, x.PeriodStart, x.PeriodEnd }).IsUnique();
        }
    }
}