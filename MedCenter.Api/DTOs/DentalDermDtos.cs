namespace MedCenter.Api.DTOs
{
    // طلب تحديث مجموعة أسنان دفعة واحدة
    public record TeethUpsertDto(
        long PatientId,
        long CenterId,
        Guid? DoctorId,
        IEnumerable<TeethPatchDto> Teeth
    );

    public record TeethPatchDto(
        string ToothCode,          // مثال: 11 أو UR6
        byte? Status,              // ToothStatus كقيمة رقمية (اختياري إن تحديث حالة فقط)
        string? VisualStateJson,   // JSON للتلوين/الأسطح/الأيقونات
        string? Notes              // ملاحظة اختياري
    );

    // طلب تحديث مجموعة مناطق جلدية
    public record RegionsUpsertDto(
        long PatientId,
        long CenterId,
        Guid? DoctorId,
        IEnumerable<RegionPatchDto> Regions
    );

    public record RegionPatchDto(
        string RegionCode,         // مثال: Face.LeftCheek
        byte? Status,              // RegionStatus كقيمة رقمية
        string? VisualStateJson,   // JSON
        string? Notes
    );
}