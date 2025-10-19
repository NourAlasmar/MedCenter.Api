// 💵واجهة خدمة الشؤون المالية (IFinanceService)
//
// الغرض:
// تحديد العمليات المالية الأساسية في النظام مثل إنشاء الفواتير، تسجيل الدفعات، وحساب أرصدة المرضى.
// تمثل هذه الواجهة العقد الذي يجب أن تلتزم به أي خدمة مالية في النظام (مثل FinanceService).
//
// الفوائد:
//  تفصل منطق العمل (Business Logic) المالي عن طبقة البيانات (Repositories).
//  تسهّل الاختبار (Unit Testing) عبر الاعتماد على واجهة بدلاً من كود التنفيذ المباشر.
//  تسمح بتطوير أو استبدال المنطق المالي لاحقًا (مثل التكامل مع بوابات الدفع الإلكترونية).
//
// العمليات:
//
//  CreateInvoiceAsync : إنشاء فاتورة جديدة
//     - تستقبل بيانات الفاتورة (InvoiceCreateDto) بما في ذلك المريض، الطبيب، والإجراءات.
//     - تُنشئ كائن الفاتورة وتُدرج عناصرها (InvoiceItems) وتحسب الإجمالي (TotalAmount).
//     - تُعيد كائن الفاتورة بعد حفظها في قاعدة البيانات.
//
//  AddPaymentAsync : تسجيل دفعة مالية
//     - تستقبل بيانات الدفعة (PaymentCreateDto) وتُنشئ كائن Payment.
//     - تُحدث رصيد الفاتورة (PaidAmount) بعد الدفع.
//     - تُعيد كائن الدفعة بعد حفظها.
//
//  GetPatientBalanceAsync : حساب الرصيد المالي للمريض
//     - تجمع جميع الفواتير المرتبطة بالمريض وتحسب المبلغ المستحق (Total - Paid).
//     - تُعيد المجموع النهائي كقيمة نقدية (decimal).


using MedCenter.Api.DTOs;
using MedCenter.Api.Models;

namespace MedCenter.Api.Services.Interfaces
{
    public interface IFinanceService
    {
        Task<Invoice> CreateInvoiceAsync(InvoiceCreateDto dto, CancellationToken ct = default);
        Task<Payment> AddPaymentAsync(PaymentCreateDto dto, CancellationToken ct = default);
        Task<decimal> GetPatientBalanceAsync(long patientId, CancellationToken ct = default);
    }
}