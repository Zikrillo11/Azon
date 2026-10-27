
using Azon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Azon.DAL.Configurations;

public sealed class NotificationLogConfiguration
    : IEntityTypeConfiguration<NotificationLog>
{
    public void Configure(EntityTypeBuilder<NotificationLog> builder)
    {
        builder.ToTable("NotificationLogs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PrayerDate)
            .IsRequired();

        builder.Property(x => x.PrayerType)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.ErrorMessage)
            .HasMaxLength(2000);

        builder.HasIndex(x => new
        {
            x.UserId,
            x.PrayerDate,
            x.PrayerType
        })
        .IsUnique();

        builder.HasIndex(x => new { x.Status, x.PrayerDate });

        builder.HasOne(x => x.User)
            .WithMany(x => x.NotificationLogs)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
