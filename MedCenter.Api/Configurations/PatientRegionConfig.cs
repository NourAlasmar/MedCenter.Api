using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedCenter.Api.Models;

namespace MedCenter.Api.Configurations
{
    // تكوين جدول مناطق الجلد/الوجه للمريض
    public class PatientRegionConfig : IEntityTypeConfiguration<PatientRegion>
    {
        public void Configure(EntityTypeBuilder<PatientRegion> b)
        {
            CommonCfg.Base(b, "PatientRegions");
            b.Property(x => x.RegionCode).IsRequired().HasMaxLength(50);
            b.Property(x => x.Status).HasConversion<byte>();
            b.Property(x => x.VisualStateJson);
            b.Property(x => x.Notes).HasMaxLength(400);
            b.HasIndex(x => new { x.PatientId, x.RegionCode }).IsUnique();
            b.HasIndex(x => x.CenterId);
        }
    }
}