// هذا الكلاس يمثل إعدادات تكوين جدول خطط العلاج (TreatmentPlans) في قاعدة البيانات
// الغرض منه هو تخزين الخطط العلاجية المقترحة لكل مريض قبل تنفيذ العلاج الفعلي.
// كل سجل يمثل خطة علاجية تم إعدادها من قبل الطبيب، وتتضمن الإجراءات المقترحة، التكلفة، وحالتها (مؤكدة، قيد الانتظار، منتهية...).
// الجدول يُستخدم في صفحات الطبيب والمريض لتتبع حالة الخطط العلاجية ومراحل تنفيذها.
// كما يساعد في الإدارة المالية لتقدير الإيرادات المتوقعة بناءً على الخطط الموافق عليها.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class TreatmentPlanConfig : IEntityTypeConfiguration<TreatmentPlan>
    {
        public void Configure(EntityTypeBuilder<TreatmentPlan> b)
        {
            // تطبيق الإعدادات العامة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "TreatmentPlans");

            // العمود Status يُمثل حالة الخطة العلاجية (مثل: مسودة، مؤكدة، منجزة، ملغاة)
            // يتم تحويله من enum إلى byte لتخزينه كقيمة رقمية صغيرة
            b.Property(x => x.Status).HasConversion<byte>();

            // العمود CreatedAtUtc يُخزن تاريخ ووقت إنشاء الخطة بالتوقيت العالمي (UTC)
            // نوع datetime2(3) يوفر دقة عالية حتى أجزاء من الثانية
            b.Property(x => x.CreatedAtUtc).HasColumnType("datetime2(3)");

            // إنشاء فهرس (Index) يجمع بين PatientId و Status
            // الهدف: تسريع عمليات البحث عن الخطط العلاجية لمريض معين بناءً على حالتها
            // مفيد في واجهة الطبيب والإدارة لمتابعة الخطط قيد التنفيذ أو المنتهية
            b.HasIndex(x => new { x.PatientId, x.Status });
        }
    }
}