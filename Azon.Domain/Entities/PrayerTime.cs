
using Azon.Domain.Common;

namespace Azon.Domain.Entities;

public class PrayerTime : BaseEntity
{
    public long CityId { get; set; }

    public DateOnly PrayerDate { get; set; }

    public TimeOnly Fajr { get; set; }

    public TimeOnly Sunrise { get; set; }

    public TimeOnly Dhuhr { get; set; }

    public TimeOnly Asr { get; set; }

    public TimeOnly Maghrib { get; set; }

    public TimeOnly Isha { get; set; }

    public string? CalculationMethod { get; set; }

    public string? Source { get; set; }

    public City City { get; set; } = null!;
}
