namespace MedCenter.Api.Models
{
    // نوع الاشتراك للمركز
    public enum SubscriptionType : byte { Monthly = 1, Yearly = 2, Custom = 3 }

    // نوع دور المستخدم داخل المركز
    public enum CenterRoleType : byte { Admin = 1, DataEntry = 2, Support = 3, Doctor = 4, Reception = 5, Accountant = 6, Investor = 7 }

    // نوع الموعد
    public enum AppointmentType : byte { Consultation = 1, Surgery = 2, FollowUp = 3, Procedure = 4 }

    // حالة الموعد
    public enum AppointmentStatus : byte { Confirmed = 1, Cancelled = 2, Completed = 3, NoShow = 4 }

    // طرق الدفع
    public enum PaymentMethod : byte { Cash = 1, Card = 2, BankTransfer = 3, Installments = 4, Other = 9 }

    // حالة المصروف / القسط
    public enum PaymentStatus : byte { Unpaid = 0, PartiallyPaid = 1, Paid = 2 }

    // أنواع المصروف
    public enum ExpenseCategory : byte { Rent = 1, Tax = 2, Food = 3, Equipment = 4, Utilities = 5, Other = 9 }

    // حالة خطة العلاج
    public enum PlanStatus : byte { Draft = 1, Confirmed = 2, PartiallyExecuted = 3, Completed = 4 }

    // قناة الإشعار
    public enum NotifyChannel : byte { InApp = 1, Sms = 2, Email = 3 }

    // حالة إرسال الرسالة
    public enum SendStatus : byte { Pending = 0, Sent = 1, Failed = 2 }

    // نوع القالب الطباعي
    public enum BrandingTemplateType : byte { Invoice = 1, Prescription = 2, Report = 3, GeneralPrint = 4 }

    // نوع حركة المخزون
    public enum InventoryMovementType : byte { Purchase = 1, Consumption = 2, Adjustment = 3, Return = 4 }

    // تقييم (1..5)
    public enum RatingScore : byte { One = 1, Two = 2, Three = 3, Four = 4, Five = 5 }
}