// هذا الكلاس CommonCfg يُعتبر مساعدًا عامًا يُستخدم في جميع ملفات Configurations
// الغرض منه هو توحيد الإعدادات الأساسية لكل الجداول داخل النظام.
// بدلاً من تكرار نفس الأكواد في كل كلاس (مثل إعداد المفتاح الأساسي والتواريخ والحذف المنطقي)
// نقوم باستدعاء CommonCfg.Base(b, "اسم_الجدول") لتطبيق الإعدادات المشتركة.
// هذا يسهّل صيانة الكود ويضمن أن جميع الجداول تلتزم بنفس المعايير والبنية في قاعدة البيانات.
//
// المهام الأساسية التي يقوم بها:
// 1. تحديد اسم الجدول في قاعدة البيانات.
// 2. تحديد المفتاح الأساسي (Id).
// 3. تفعيل الفلتر العام للحذف المنطقي (IsDeleted = false).
// 4. ضبط أنواع الأعمدة الزمنية (CreatedAt / UpdatedAt / DeletedAt) لتكون datetime2(3).
// هذا الأسلوب يختصر الأكواد ويحافظ على اتساق قاعدة البيانات في جميع الجداول.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    /// <summary>
    /// مساعد يطبّق إعدادات مشتركة لكل الجداول (اسم الجدول + المفاتيح + الحذف المنطقي + الدقة الزمنية).
    /// </summary>
    internal static class CommonCfg
    {
        public static void Base<TEntity>(EntityTypeBuilder<TEntity> b, string table)
            where TEntity : BaseEntity
        {
            // تحديد اسم الجدول في قاعدة البيانات
            b.ToTable(table);

            // تحديد العمود Id كمفتاح أساسي للجدول (Primary Key)
            b.HasKey(x => x.Id);

            // تطبيق فلتر عام تلقائي على جميع الاستعلامات لاستبعاد السجلات المحذوفة منطقياً (Soft Delete)
            b.HasQueryFilter(x => !x.IsDeleted);

            // تحديد نوع أعمدة التواريخ بدقة 3 خانات عشرية (milliseconds)
            b.Property(x => x.CreatedAt).HasColumnType("datetime2(3)");
            b.Property(x => x.UpdatedAt).HasColumnType("datetime2(3)");
            b.Property(x => x.DeletedAt).HasColumnType("datetime2(3)");
        }
    }
}