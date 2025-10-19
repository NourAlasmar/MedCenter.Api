//  خدمة الشؤون المالية (FinanceService)
//
// الغرض:
// إدارة المعاملات المالية داخل النظام، بما في ذلك إنشاء الفواتير، تسجيل المدفوعات، وحساب الأرصدة.
//
// تعتمد على وحدة العمل (IUnitOfWork) للوصول إلى المستودعات الخاصة بالفواتير والمدفوعات والعناصر المالية.
// تُستخدم من قِبل النظام في عمليات الفوترة للمراكز الطبية، أو واجهات الأطباء والإدارة.
//
// الوظائف الأساسية:
//
//  CreateInvoiceAsync : إنشاء فاتورة جديدة للمريض
//     - تُنشئ كائن Invoice وتولّد رقم فاتورة فريد تلقائيًا (INV-YYYYMMDDHHmmss).
//     - تُضيف العناصر (InvoiceItems) بناءً على محتوى الـ DTO.
//     - تحسب المجموع الكلي للفاتورة (TotalAmount) بعد تطبيق الخصومات.
//     - تحفظ جميع البيانات في قاعدة البيانات ضمن معاملة واحدة.
//
//  AddPaymentAsync : تسجيل دفعة مالية لفاتورة
//     - تُنشئ كائن Payment جديد وتربطه بالفاتورة.
//     - تُحدث المبلغ المدفوع (PaidAmount) في كيان الفاتورة.
//     - تُستخدم عادة عند دفع المريض جزءًا من الفاتورة أو المبلغ كاملاً.
//     - يمكن توسيعها لاحقًا لإرسال إيصال أو إشعار SMS.
//
//  GetPatientBalanceAsync : حساب الرصيد المالي للمريض
//     - تُجمع كل فواتير المريض وتحسب الفرق بين TotalAmount و PaidAmount.
//     - الناتج يُمثل المبلغ المستحق على المريض (ديون غير مدفوعة).

using MedCenter.Api.DTOs;
using MedCenter.Api.Models;
using MedCenter.Api.Repositories.Interfaces;
using MedCenter.Api.Services.Interfaces;

namespace MedCenter.Api.Services.Implementations
{
    public class FinanceService : IFinanceService
    {
        private readonly IUnitOfWork _uow;
        public FinanceService(IUnitOfWork uow) => _uow = uow;

        public async Task<Invoice> CreateInvoiceAsync(InvoiceCreateDto dto, CancellationToken ct = default)
        {
            var inv = new Invoice
            {
                CenterId = dto.CenterId,
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                CurrencyCode = dto.CurrencyCode,
                InvoiceDate = DateTime.UtcNow,
                InvoiceNo = $"INV-{DateTime.UtcNow:yyyyMMddHHmmss}"
            };
            await _uow.Invoices.AddAsync(inv, ct);
            await _uow.SaveAsync(ct);

            decimal total = 0m;
            foreach (var it in dto.Items)
            {
                var line = new InvoiceItem
                {
                    InvoiceId = inv.Id,
                    TreatmentId = it.TreatmentId,
                    ProcedureId = it.ProcedureId,
                    Description = it.Description,
                    Qty = it.Qty,
                    UnitPrice = it.UnitPrice,
                    Discount = it.Discount
                };
                total += (it.UnitPrice * it.Qty) - (it.Discount ?? 0m);
                await _uow.InvoiceItems.AddAsync(line, ct);
            }
            inv.TotalAmount = total;
            await _uow.SaveAsync(ct);
            return inv;
        }

        public async Task<Payment> AddPaymentAsync(PaymentCreateDto dto, CancellationToken ct = default)
        {
            var p = new Payment
            {
                InvoiceId = dto.InvoiceId,
                PaymentDate = dto.PaymentDate,
                Amount = dto.Amount,
                Method = dto.Method,
                Notes = dto.Notes
            };
            await _uow.Payments.AddAsync(p, ct);

            var inv = await _uow.Invoices.GetByIdAsync(dto.InvoiceId, ct);
            if (inv != null)
            {
                inv.PaidAmount += dto.Amount;
                _uow.Invoices.Update(inv);
            }

            await _uow.SaveAsync(ct);
            return p;
        }

        public async Task<decimal> GetPatientBalanceAsync(long patientId, CancellationToken ct = default)
        {
            var invoices = await _uow.Invoices.GetAsync(i => i.PatientId == patientId, ct: ct);
            return invoices.Sum(i => i.TotalAmount - i.PaidAmount);
        }
    }
}