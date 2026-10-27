
using Azon.Domain.Common;
using Azon.Domain.Enums;

namespace Azon.Domain.Entities;

public class City : BaseEntity
{
    public required string Name { get; set; }

    public required string CountryCode { get; set; }

    public required string TimeZoneId { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<UserPreference> UserPreferences { get; set; }
        = new List<UserPreference>();

    public ICollection<Mosque> Mosques { get; set; }
        = new List<Mosque>();

    public ICollection<PrayerTime> PrayerTimes { get; set; }
        = new List<PrayerTime>();
}
