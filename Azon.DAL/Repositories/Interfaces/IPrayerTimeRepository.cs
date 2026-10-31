using Azon.Domain.Entities;

namespace Azon.DAL.Repositories.Interfaces;

public interface IPrayerTimeRepository
{
    Task<PrayerTime?> GetByIdAsync(long id);
    Task<PrayerTime?> GetByCityAndDateAsync(long cityId, DateOnly date);
    Task<IEnumerable<PrayerTime>> GetByCityIdAsync(long cityId);
    Task<PrayerTime> CreateAsync(PrayerTime prayerTime);
    Task<PrayerTime> UpdateAsync(PrayerTime prayerTime);
    Task<bool> DeleteAsync(long id);
}