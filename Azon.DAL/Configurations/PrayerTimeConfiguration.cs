
using Azon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Azon.DAL.Configurations;

public sealed class PrayerTimeConfiguration
    : IEntityTypeConfiguration<PrayerTime>
{
    public void Configure(EntityTypeBuilder<PrayerTime> builder)
    {
        builder.ToTable("PrayerTimes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PrayerDate)
            .IsRequired();

        builder.Property(x => x.CalculationMethod)
            .HasMaxLength(100);

        builder.Property(x => x.Source)
            .HasMaxLength(100);

        builder.HasIndex(x => new { x.CityId, x.PrayerDate })
            .IsUnique();

        builder.HasOne(x => x.City)
            .WithMany(x => x.PrayerTimes)
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
