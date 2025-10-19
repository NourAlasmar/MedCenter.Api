using MedCenter.Api.Models;

namespace MedCenter.Api.Repositories.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<Center> Centers { get; }
        IGenericRepository<CenterUser> CenterUsers { get; }
        IGenericRepository<Patient> Patients { get; }
        IGenericRepository<Appointment> Appointments { get; }

        // ✅ جديد
        IGenericRepository<Treatment> Treatments { get; }

        IGenericRepository<Invoice> Invoices { get; }
        IGenericRepository<InvoiceItem> InvoiceItems { get; }
        IGenericRepository<Payment> Payments { get; }
        IGenericRepository<Expense> Expenses { get; }
        IGenericRepository<ExpensePayment> ExpensePayments { get; }
        IGenericRepository<Refund> Refunds { get; }
        IGenericRepository<Lab> Labs { get; }
        IGenericRepository<LabOrder> LabOrders { get; }
        IGenericRepository<ProcedureCatalog> ProcedureCatalogs { get; }
        IGenericRepository<CenterProcedurePrice> CenterProcedurePrices { get; }

        // ✅ جداول الحالة البصرية (الأسنان / الجلد)
        IGenericRepository<PatientTooth> PatientTeeth { get; }
        IGenericRepository<PatientRegion> PatientRegions { get; }

        Task<int> SaveAsync(CancellationToken ct = default);
    }
}