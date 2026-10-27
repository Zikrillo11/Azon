
using Azon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Azon.DAL.Configurations;

public sealed class UserPreferenceConfiguration
    : IEntityTypeConfiguration<UserPreference>
{
    public void Configure(EntityTypeBuilder<UserPreference> builder)
    {
        builder.ToTable("UserPreferences");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.UserId)
            .IsUnique();

        builder.Property(x => x.NotificationsEnabled)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.FajrNotificationEnabled)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.DhuhrNotificationEnabled)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.AsrNotificationEnabled)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.MaghribNotificationEnabled)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.IshaNotificationEnabled)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasOne(x => x.City)
            .WithMany(x => x.UserPreferences)
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
