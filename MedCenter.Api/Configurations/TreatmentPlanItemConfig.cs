// هذا الكلاس يمثل إعدادات تكوين جدول عناصر الخطة العلاجية (TreatmentPlanItems) في قاعدة البيانات
// الغرض منه هو تخزين تفاصيل كل إجراء أو خدمة مضمّنة داخل خطة علاجية معينة.
// كل سجل في هذا الجدول يمثل بندًا واحدًا في الخطة، مثل "حشوة سن علوي"، "تنظيف الأسنان"، "حقن بوتوكس"، إلخ.
// الجدول يُستخدم لتقسيم الخطة العلاجية إلى إجراءات تفصيلية يمكن تنفيذها لاحقًا بشكل مستقل أو مرحلي.
// كما يُفيد في تتبع التكاليف الجزئية لكل عنصر وربطها مع الإجراءات المنفذة لاحقًا في جدول Treatments.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    public class TreatmentPlanItemConfig : IEntityTypeConfiguration<TreatmentPlanItem>
    {
        public void Configure(EntityTypeBuilder<TreatmentPlanItem> b)
        {
            // تطبيق الإعدادات العامة مثل المفتاح الأساسي، الحذف المنطقي، وتواريخ الإنشاء والتعديل
            CommonCfg.Base(b, "TreatmentPlanItems");

            // العمود ToothCode يُستخدم في حال كان الإجراء متعلقًا بالأسنان (مثل "UR6" أي الضرس العلوي الأيمن السادس)
            // اختياري بطول أقصى 10 أحرف
            b.Property(x => x.ToothCode).HasMaxLength(10);

            // العمود AreaCode يُستخدم لتحديد المنطقة في الجسم (للجلدية أو التجميل مثلاً)
            // مثل "Face.Left" أو "Abdomen"
            // اختياري بطول أقصى 20 حرفًا
            b.Property(x => x.AreaCode).HasMaxLength(20);

            // إنشاء فهرس (Index) على PlanId
            // الهدف: تسريع عمليات البحث عن جميع العناصر المرتبطة بخطة علاجية معينة
            // مفيد عند تحميل تفاصيل الخطة في واجهة الطبيب أو الإدارة
            b.HasIndex(x => x.PlanId);
        }
    }
}