using System;

namespace MedCenter.Api.Models
{
    /// <summary>
    /// انتساب مستخدم (AspNetUser) إلى مركز معيّن + الدور المحلي داخل المركز.
    /// </summary>
    public class CenterUser : BaseEntity
    {
        public long CenterId { get; set; }
        public Guid UserId { get; set; } // من AspNetUsers.Id (GUID)

        public CenterRoleType RoleType { get; set; } = CenterRoleType.Doctor;
        public bool IsOwner { get; set; } = false;

        public DateTime JoinedAt { get; set; }
        public DateTime? LeftAt { get; set; }
        public bool IsActive { get; set; } = true;

        public DateTime? SubscriptionEndDate { get; set; } // تمديد اشتراك حساب المستخدم داخل المركز

        // Nav (تعريف العلاقات في Configurations)
        public Center? Center { get; set; }
    }
}