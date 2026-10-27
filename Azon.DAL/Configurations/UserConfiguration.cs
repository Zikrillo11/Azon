
using Azon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Azon.DAL.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.TelegramUserId)
            .IsRequired();

        builder.HasIndex(x => x.TelegramUserId)
            .IsUnique();

        builder.Property(x => x.TelegramUsername)
            .HasMaxLength(100);

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .HasMaxLength(100);

        builder.Property(x => x.LanguageCode)
            .IsRequired()
            .HasMaxLength(5)
            .HasDefaultValue("uz");

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.HasOne(x => x.Preference)
            .WithOne(x => x.User)
            .HasForeignKey<UserPreference>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.NotificationLogs)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
