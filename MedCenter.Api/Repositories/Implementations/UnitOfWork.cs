// تنفيذ وحدة العمل (UnitOfWork):
// - ينسّق العمل بين جميع المستودعات Repositories باستخدام نفس AppDbContext.
// - يوفّر خصائص للوصول إلى كل جدول نحتاجه (CRUD عام عبر GenericRepository).
// - تمت إضافة مستودعات المخطط البصري للمريض: PatientTeeth و PatientRegions.
// - تمت إضافة مستودع العلاجات Treatments لحل الاستدعاء من TreatmentService.
// - استخدم SaveAsync لحفظ جميع التغييرات دفعة واحدة.

using MedCenter.Api.Data;
using MedCenter.Api.Models;
using MedCenter.Api.Repositories.Interfaces;

namespace MedCenter.Api.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _ctx;

        public UnitOfWork(AppDbContext ctx)
        {
            _ctx = ctx;

            // جداول الإدارة/المركز
            Centers = new GenericRepository<Center>(_ctx);
            CenterUsers = new GenericRepository<CenterUser>(_ctx);

            // المرضى والمواعيد والعلاجات
            Patients = new GenericRepository<Patient>(_ctx);
            Appointments = new GenericRepository<Appointment>(_ctx);
            Treatments = new GenericRepository<Treatment>(_ctx);      // ✅ جديد: مستودع العلاجات

            // الفواتير والمالية
            Invoices = new GenericRepository<Invoice>(_ctx);
            InvoiceItems = new GenericRepository<InvoiceItem>(_ctx);
            Payments = new GenericRepository<Payment>(_ctx);
            Expenses = new GenericRepository<Expense>(_ctx);
            ExpensePayments = new GenericRepository<ExpensePayment>(_ctx);
            Refunds = new GenericRepository<Refund>(_ctx);

            // المخابر
            Labs = new GenericRepository<Lab>(_ctx);
            LabOrders = new GenericRepository<LabOrder>(_ctx);

            // الدلائل والتسعير
            ProcedureCatalogs = new GenericRepository<ProcedureCatalog>(_ctx);
            CenterProcedurePrices = new GenericRepository<CenterProcedurePrice>(_ctx);

            // ✅ جداول الحالة البصرية (الأسنان/الجلد)
            PatientTeeth = new GenericRepository<PatientTooth>(_ctx);
            PatientRegions = new GenericRepository<PatientRegion>(_ctx);
        }

        // -------- خصائص المستودعات (Repositories) --------

        // الإدارة/المركز
        public IGenericRepository<Center> Centers { get; }
        public IGenericRepository<CenterUser> CenterUsers { get; }

        // المرضى والمواعيد والعلاجات
        public IGenericRepository<Patient> Patients { get; }
        public IGenericRepository<Appointment> Appointments { get; }
        public IGenericRepository<Treatment> Treatments { get; }                  // ✅ جديد

        // الفواتير والمالية
        public IGenericRepository<Invoice> Invoices { get; }
        public IGenericRepository<InvoiceItem> InvoiceItems { get; }
        public IGenericRepository<Payment> Payments { get; }
        public IGenericRepository<Expense> Expenses { get; }
        public IGenericRepository<ExpensePayment> ExpensePayments { get; }
        public IGenericRepository<Refund> Refunds { get; }

        // المخابر
        public IGenericRepository<Lab> Labs { get; }
        public IGenericRepository<LabOrder> LabOrders { get; }

        // الدلائل والتسعير
        public IGenericRepository<ProcedureCatalog> ProcedureCatalogs { get; }
        public IGenericRepository<CenterProcedurePrice> CenterProcedurePrices { get; }

        // ✅ الحالة البصرية (الأسنان/الجلد)
        public IGenericRepository<PatientTooth> PatientTeeth { get; }
        public IGenericRepository<PatientRegion> PatientRegions { get; }

        // حفظ/إتلاف
        public Task<int> SaveAsync(CancellationToken ct = default) => _ctx.SaveChangesAsync(ct);
        public ValueTask DisposeAsync() => _ctx.DisposeAsync();
    }
}