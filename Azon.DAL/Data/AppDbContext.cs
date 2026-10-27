using Azon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Azon.DAL.DAL;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<UserPreference> UserPreferences => Set<UserPreference>();
    public DbSet<City> Cities => Set<City>();
    public DbSet<Mosque> Mosques => Set<Mosque>();
    public DbSet<PrayerTime> PrayerTimes => Set<PrayerTime>();
    public DbSet<NotificationLog> NotificationLogs => Set<NotificationLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);
    }
}
