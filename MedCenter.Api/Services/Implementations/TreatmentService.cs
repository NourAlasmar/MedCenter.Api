// خدمة العلاجات: عند إضافة علاج، نقوم بتحديث حالة السن/المنطقة بصريًا:
// - الأسنان: Planned -> Confirmed -> Completed (حسب الحالة)، هنا نجعلها Completed فور التنفيذ.
// - الجلدية: Normal/Planned -> Treated.
// كما نحدّث VisualStateJson بلون وأيقونة مناسبة (اختصارًا — يمكن تخصيصه لاحقًا).

using System.Text.Json;
using MedCenter.Api.DTOs;
using MedCenter.Api.Models;
using MedCenter.Api.Repositories.Interfaces;
using MedCenter.Api.Services.Interfaces;

namespace MedCenter.Api.Services.Implementations
{
    public class TreatmentService : ITreatmentService
    {
        private readonly IUnitOfWork _uow;
        public TreatmentService(IUnitOfWork uow) => _uow = uow;

        public async Task<Treatment> AddAsync(TreatmentCreateDto dto, CancellationToken ct = default)
        {
            // 1) إنشاء سجل العلاج
            var t = new Treatment
            {
                CenterId = dto.CenterId,
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                ProcedureId = dto.ProcedureId,
                ToothCode = dto.ToothCode,
                AreaCode = dto.AreaCode,
                ExecutedAt = dto.ExecutedAtUtc ?? DateTime.UtcNow,
                Notes = dto.Notes,
                Price = dto.Price ?? 0m
            };

            await _uow.Treatments.AddAsync(t, ct);
            await _uow.SaveAsync(ct);
            //
            // 2) تحديث الحالة البصرية للسن/المنطقة حسب المعطيات
            if (!string.IsNullOrWhiteSpace(dto.ToothCode))
                await UpsertToothVisualAsync(dto, ct);
            else if (!string.IsNullOrWhiteSpace(dto.AreaCode))
                await UpsertRegionVisualAsync(dto, ct);

            await _uow.SaveAsync(ct);
            return t;
        }

        // -- تحديث/إدراج حالة سن
        private async Task UpsertToothVisualAsync(TreatmentCreateDto dto, CancellationToken ct)
        {
            var tooth = (await _uow.PatientTeeth.GetAsync(
                x => x.PatientId == dto.PatientId && x.ToothCode == dto.ToothCode!, ct: ct)).FirstOrDefault();

            if (tooth is null)
            {
                tooth = new PatientTooth
                {
                    CenterId = dto.CenterId,
                    PatientId = dto.PatientId,
                    DoctorId = dto.DoctorId,
                    ToothCode = dto.ToothCode!,
                    Status = ToothStatus.Completed, // مباشرة بعد تنفيذ العلاج
                    VisualStateJson = BuildToothVisualJson(status: ToothStatus.Completed, icon: "check", color: "#4CAF50"),
                    Notes = dto.Notes
                };
                await _uow.PatientTeeth.AddAsync(tooth, ct);
            }
            else
            {
                tooth.Status = ToothStatus.Completed;
                tooth.VisualStateJson = BuildToothVisualJson(status: ToothStatus.Completed, icon: "check", color: "#4CAF50", existing: tooth.VisualStateJson);
                if (!string.IsNullOrWhiteSpace(dto.Notes)) tooth.Notes = dto.Notes;
                tooth.UpdatedAt = DateTime.UtcNow;
                _uow.PatientTeeth.Update(tooth);
            }
        }

        // -- تحديث/إدراج حالة منطقة جلد/وجه
        private async Task UpsertRegionVisualAsync(TreatmentCreateDto dto, CancellationToken ct)
        {
            var region = (await _uow.PatientRegions.GetAsync(
                x => x.PatientId == dto.PatientId && x.RegionCode == dto.AreaCode!, ct: ct)).FirstOrDefault();

            if (region is null)
            {
                region = new PatientRegion
                {
                    CenterId = dto.CenterId,
                    PatientId = dto.PatientId,
                    DoctorId = dto.DoctorId,
                    RegionCode = dto.AreaCode!,
                    Status = RegionStatus.Treated,
                    VisualStateJson = BuildRegionVisualJson(status: RegionStatus.Treated, icon: "needle", color: "#4CAF50"),
                    Notes = dto.Notes
                };
                await _uow.PatientRegions.AddAsync(region, ct);
            }
            else
            {
                region.Status = RegionStatus.Treated;
                region.VisualStateJson = BuildRegionVisualJson(status: RegionStatus.Treated, icon: "needle", color: "#4CAF50", existing: region.VisualStateJson);
                if (!string.IsNullOrWhiteSpace(dto.Notes)) region.Notes = dto.Notes;
                region.UpdatedAt = DateTime.UtcNow;
                _uow.PatientRegions.Update(region);
            }
        }

        private static string BuildToothVisualJson(ToothStatus status, string icon, string color, string? existing = null)
        {
            var obj = string.IsNullOrWhiteSpace(existing)
                ? new Dictionary<string, object?>()
                : JsonSerializer.Deserialize<Dictionary<string, object?>>(existing!) ?? new();

            obj["status"] = status.ToString();
            obj["color"] = color;              // لون أخضر عند الإتمام
            obj["icons"] = new[] { icon };     // مثال: "check", "crown", "filling"...
            return JsonSerializer.Serialize(obj);
        }

        // -- توليد JSON بسيط لحالة المنطقة الجلدية
        private static string BuildRegionVisualJson(RegionStatus status, string icon, string color, string? existing = null)
        {
            var obj = string.IsNullOrWhiteSpace(existing)
                ? new Dictionary<string, object?>()
                : JsonSerializer.Deserialize<Dictionary<string, object?>>(existing!) ?? new();

            obj["status"] = status.ToString();
            obj["color"] = color;
            obj["icons"] = new[] { icon };     // مثال: "needle", "laser", "scissor"
            return JsonSerializer.Serialize(obj);
        }
    }
}