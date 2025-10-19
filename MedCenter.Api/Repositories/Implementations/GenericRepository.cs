// هذا الكلاس هو تنفيذ للمستودع العام Generic Repository لنماذج ترث من BaseEntity
// الهدف: توفير عمليات CRUD واستعلامات عامة قابلة لإعادة الاستخدام لكل الجداول
// بدون تكرار منطق EF Core في كل خدمة.
// يعتمد على AppDbContext و DbSet<T> للتعامل مع البيانات.
// ملاحظات:
// - يدعم الفلاتر (predicate) والترتيب (orderBy) والـ include (بالسلسلة) مع التصفح (skip/take).
// - يستخدم Soft Delete عبر خاصية IsDeleted المُعرّفة في BaseEntity ويستفيد من HasQueryFilter في CommonCfg.
// - احرص على تمرير CancellationToken في المسارات الطويلة (Web/API) لتحسين الاستجابة تحت الضغط.

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MedCenter.Api.Data;
using MedCenter.Api.Models;
using MedCenter.Api.Repositories.Interfaces;

namespace MedCenter.Api.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _ctx; // مرجع لسياق قاعدة البيانات
        protected readonly DbSet<T> _db;      // المجموعة (الجدول) الخاصة بالنوع T

        public GenericRepository(AppDbContext ctx)
        {
            _ctx = ctx;
            _db = _ctx.Set<T>(); // الحصول على DbSet<T> من السياق
        }

        // جلب سجل واحد حسب المفتاح Id (يراعي فلتر الحذف المنطقي تلقائياً)
        public async Task<T?> GetByIdAsync(long id, CancellationToken ct = default)
            => await _db.FirstOrDefaultAsync(x => x.Id == id, ct);

        // جلب قائمة سجلات مع خيارات:
        // predicate: فلتر WHERE اختياري
        // orderBy: ترتيب اختياري
        // includeProperties: أسماء الملاحة (Navigation Properties) مفصولة بفواصل
        // take/skip: للتقسيم إلى صفحات
        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "", int? take = null, int? skip = null, CancellationToken ct = default)
        {
            IQueryable<T> q = _db.AsQueryable(); // بدء الاستعلام من الجدول

            if (predicate != null) q = q.Where(predicate); // تطبيق الشرط إذا وُجد

            // تضمين الملاحة (Include) من خلال أسماء خاصيات مفصولة بفواصل
            foreach (var includeProp in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                q = q.Include(includeProp.Trim());

            if (orderBy != null) q = orderBy(q);   // ترتيب اختياري
            if (skip.HasValue) q = q.Skip(skip.Value); // تجاوز عدد سجلات (للترقيم)
            if (take.HasValue) q = q.Take(take.Value); // أخذ عدد محدد (للترقيم)

            return await q.ToListAsync(ct); // تنفيذ الاستعلام وإرجاع النتيجة
        }

        // التأكد من وجود أي سجل يحقق الشرط
        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
            => _db.AnyAsync(predicate, ct);

        // عدّ السجلات (مع شرط اختياري)
        public Task<long> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken ct = default)
            => predicate == null ? _db.LongCountAsync(ct) : _db.LongCountAsync(predicate, ct);

        // إضافة سجل جديد (AsTask لتحويل ValueTask إلى Task)
        public Task AddAsync(T entity, CancellationToken ct = default) => _db.AddAsync(entity, ct).AsTask();

        // إضافة مجموعة سجلات دفعة واحدة
        public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default) => _db.AddRangeAsync(entities, ct);

        // تحديث سجل (EF يقوم بتتبع الكيان وتعديل حالته إلى Modified)
        public void Update(T entity) => _db.Update(entity);

        // حذف فعلي من قاعدة البيانات (ليس Soft Delete)
        public void Remove(T entity) => _db.Remove(entity);

        // حذف منطقي: لا يتم إزالة السجل من القاعدة، فقط يتم تعليم IsDeleted = true
        // ويُملأ DeletedAt و DeletedBy. الاستعلامات العامة تستثنيه بفضل HasQueryFilter في CommonCfg.
        public void SoftDelete(T entity, long? userId = null)
        {
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow; // تخزين وقت الحذف بالتوقيت العالمي
            entity.DeletedBy = userId;          // من قام بالحذف (اختياري)
            _db.Update(entity);                 // تعليم الكيان كمعدل ليتم حفظه
        }
    }
}