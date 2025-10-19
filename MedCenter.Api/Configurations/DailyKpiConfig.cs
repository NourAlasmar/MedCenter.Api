// هذا الكلاس يمثل إعدادات تكوين جدول مؤشرات الأداء اليومية (DailyKpis) في قاعدة البيانات
// الغرض منه هو تخزين بيانات الأداء اليومي للمركز الطبي
// مثل عدد المرضى الذين تمت معاينتهم، إجمالي الدخل اليومي، عدد الإجراءات المنفذة، أو تقييم الأطباء
// هذه البيانات تُستخدم في لوحة التحكم (Dashboard) الخاصة بالإدارة أو المستثمرين
// لمتابعة أداء المركز بشكل يومي وتحليل التطور على مدار الشهر أو السنة.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class DailyKpiConfig : IEntityTypeConfiguration<DailyKpi>
    {
        public void Configure(EntityTypeBuilder<DailyKpi> b)
        {
            // تطبيق الإعدادات العامة مثل المفتاح الأساسي، وتواريخ الإنشاء والتعديل، والحذف المنطقي
            CommonCfg.Base(b, "DailyKpis");

            // تحديد نوع العمود Day ليكون من نوع date (تاريخ فقط بدون وقت)
            // هذا العمود يمثل تاريخ اليوم الذي تم فيه تسجيل البيانات
            b.Property(x => x.Day).HasColumnType("date");

            // إنشاء  (Composite Index) على CenterId و Day
            // الهدف: ضمان عدم وجود أكثر من سجل KPI واحد لنفس المركز في نفس اليوم
            // كما يُستخدم لتسريع عمليات البحث عند استعراض أداء مركز معين في فترة محددة
            b.HasIndex(x => new { x.CenterId, x.Day }).IsUnique();
        }
    }
}