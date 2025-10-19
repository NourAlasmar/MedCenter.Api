using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    // تكوين جدول أسنان المريض
    public class PatientToothConfig : IEntityTypeConfiguration<PatientTooth>
    {
        public void Configure(EntityTypeBuilder<PatientTooth> b)
        {
            CommonCfg.Base(b, "PatientTeeth");
            b.Property(x => x.ToothCode).IsRequired().HasMaxLength(10);
            b.Property(x => x.Status).HasConversion<byte>();
            b.Property(x => x.VisualStateJson); // نص حر (JSON)
            b.Property(x => x.Notes).HasMaxLength(400);
            b.HasIndex(x => new { x.PatientId, x.ToothCode }).IsUnique(); // سن واحد لكل كود-مريض
            b.HasIndex(x => x.CenterId);
        }
    }
}