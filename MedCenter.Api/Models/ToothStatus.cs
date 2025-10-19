namespace MedCenter.Api.Models
{
    // حالة السن على مخطط المريض (ليست تشخيصًا طبيًا فقط، بل حالة عرضية تُستخدم للتلوين/الأيقونات)
    public enum ToothStatus : byte
    {
        Healthy = 0,     // سليم
        Planned = 1,     // ضمن خطة علاج (رمادي)
        Confirmed = 2,   // تم تأكيد التنفيذ (أصفر)
        Completed = 3,   // مُنفّذ (أخضر)
        Missing = 4,     // مفقود/مقلوع
        Implant = 5      // زرعة
    }
}