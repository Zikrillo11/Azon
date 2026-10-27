
using Azon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Azon.DAL.Configurations;

public sealed class MosqueConfiguration : IEntityTypeConfiguration<Mosque>
{
    public void Configure(EntityTypeBuilder<Mosque> builder)
    {
        builder.ToTable("Mosques");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Address)
            .HasMaxLength(500);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(30);

        builder.Property(x => x.Latitude)
            .HasPrecision(9, 6);

        builder.Property(x => x.Longitude)
            .HasPrecision(9, 6);

        builder.Property(x => x.OpenStreetMapId)
            .HasMaxLength(100);

        builder.Property(x => x.IsVerified)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasIndex(x => x.OpenStreetMapId);

        builder.HasIndex(x => x.CityId);

        builder.HasOne(x => x.City)
            .WithMany(x => x.Mosques)
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
