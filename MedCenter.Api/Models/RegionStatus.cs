namespace MedCenter.Api.Models
{
    // حالة منطقة الوجه/الجلد على مخطط المريض
    public enum RegionStatus : byte
    {
        Normal = 0,      // عادي
        Planned = 1,     // ضمن خطة (رمادي)
        Confirmed = 2,   // مؤكّد (أصفر)
        Treated = 3      // مُعالج/مكتمل (أخضر)
    }
}