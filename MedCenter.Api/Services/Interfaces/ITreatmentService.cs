// واجهة خدمة العلاجات: إضافة علاج وتحديث الحالة البصرية تلقائيًا

using MedCenter.Api.DTOs;
using MedCenter.Api.Models;

namespace MedCenter.Api.Services.Interfaces
{
    public interface ITreatmentService
    {
        Task<Treatment> AddAsync(TreatmentCreateDto dto, CancellationToken ct = default);
    }
}