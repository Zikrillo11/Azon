using Azon.Shared.DTOs.PrayerTimeDTOs;

namespace Azon.BLL.Interfaces;

public interface IPrayerTimeService
{
    Task<PrayerTimeForResultDto?> GetByIdAsync(long id);

    Task<PrayerTimeForResultDto?> GetByCityAndDateAsync(
        long cityId,
        DateOnly date);

    Task<IEnumerable<PrayerTimeForResultDto>> GetByCityIdAsync(long cityId);

    Task<PrayerTimeForResultDto> CreateAsync(PrayerTimeForCreateDto dto);

    Task<PrayerTimeForResultDto?> UpdateAsync(PrayerTimeForUpdateDto dto);

    Task<bool> DeleteAsync(long id);
}