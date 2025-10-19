using Microsoft.EntityFrameworkCore;
using MedCenter.Api.Models;

namespace MedCenter.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // ========== DbSets ==========
        public DbSet<Center> Centers => Set<Center>();
        public DbSet<CenterUser> CenterUsers => Set<CenterUser>();

        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<PatientContact> PatientContacts => Set<PatientContact>();
        public DbSet<PatientFile> PatientFiles => Set<PatientFile>();
        public DbSet<PatientConsent> PatientConsents => Set<PatientConsent>();
        public DbSet<PatientDiagnosis> PatientDiagnoses => Set<PatientDiagnosis>();

        public DbSet<TreatmentPlan> TreatmentPlans => Set<TreatmentPlan>();
        public DbSet<TreatmentPlanItem> TreatmentPlanItems => Set<TreatmentPlanItem>();
        public DbSet<Treatment> Treatments => Set<Treatment>();
        public DbSet<Prescription> Prescriptions => Set<Prescription>();

        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<AppointmentReminder> AppointmentReminders => Set<AppointmentReminder>();

        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Expense> Expenses => Set<Expense>();
        public DbSet<ExpensePayment> ExpensePayments => Set<ExpensePayment>();
        public DbSet<CenterBill> CenterBills => Set<CenterBill>();
        public DbSet<CenterBillInstallment> CenterBillInstallments => Set<CenterBillInstallment>();
        public DbSet<Refund> Refunds => Set<Refund>();
        public DbSet<FinancialStatement> FinancialStatements => Set<FinancialStatement>();

        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<EmployeeContract> EmployeeContracts => Set<EmployeeContract>();
        public DbSet<Attendance> Attendances => Set<Attendance>();
        public DbSet<PayrollAdjustment> PayrollAdjustments => Set<PayrollAdjustment>();
        public DbSet<EmployeeNote> EmployeeNotes => Set<EmployeeNote>();
        public DbSet<EmployeeDocument> EmployeeDocuments => Set<EmployeeDocument>();
        public DbSet<EmployeeRating> EmployeeRatings => Set<EmployeeRating>();

        public DbSet<Lab> Labs => Set<Lab>();
        public DbSet<LabService> LabServices => Set<LabService>();
        public DbSet<LabOrder> LabOrders => Set<LabOrder>();
        public DbSet<LabSettlement> LabSettlements => Set<LabSettlement>();

        public DbSet<Specialty> Specialties => Set<Specialty>();
        public DbSet<DiagnosisCatalog> DiagnosisCatalogs => Set<DiagnosisCatalog>();
        public DbSet<ProcedureCatalog> ProcedureCatalogs => Set<ProcedureCatalog>();
        public DbSet<CenterProcedurePrice> CenterProcedurePrices => Set<CenterProcedurePrice>();

        public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
        public DbSet<InventoryBatch> InventoryBatches => Set<InventoryBatch>();
        public DbSet<InventoryMovement> InventoryMovements => Set<InventoryMovement>();

        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<SmsTemplate> SmsTemplates => Set<SmsTemplate>();
        public DbSet<SmsOutbox> SmsOutbox => Set<SmsOutbox>();

        public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();
        public DbSet<BrandingTemplate> BrandingTemplates => Set<BrandingTemplate>();
        public DbSet<PrintJob> PrintJobs => Set<PrintJob>();

        public DbSet<DailyKpi> DailyKpis => Set<DailyKpi>();
        public DbSet<Feedback> Feedbacks => Set<Feedback>();
        public DbSet<Attachment> Attachments => Set<Attachment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}