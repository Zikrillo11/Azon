using Azon.DAL.DAL;
using Azon.DAL.Repositories.Interfaces;
using Azon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Azon.DAL.Repositories.Implementations;

public class PrayerTimeRepository : IPrayerTimeRepository
{
    private readonly AppDbContext _context;

    public PrayerTimeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PrayerTime?> GetByIdAsync(long id)
    {
        return await _context.PrayerTimes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<PrayerTime?> GetByCityAndDateAsync(
        long cityId,
        DateOnly date)
    {
        return await _context.PrayerTimes
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                x.CityId == cityId &&
                x.PrayerDate == date);
    }

    public async Task<IEnumerable<PrayerTime>> GetByCityIdAsync(long cityId)
    {
        return await _context.PrayerTimes
            .AsNoTracking()
            .Where(x => x.CityId == cityId)
            .OrderBy(x => x.PrayerDate)
            .ToListAsync();
    }

    public async Task<PrayerTime> CreateAsync(PrayerTime prayerTime)
    {
        await _context.PrayerTimes.AddAsync(prayerTime);
        await _context.SaveChangesAsync();

        return prayerTime;
    }

    public async Task<PrayerTime> UpdateAsync(PrayerTime prayerTime)
    {
        _context.PrayerTimes.Update(prayerTime);
        await _context.SaveChangesAsync();

        return prayerTime;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var prayerTime = await _context.PrayerTimes.FindAsync(id);

        if (prayerTime is null)
            return false;

        _context.PrayerTimes.Remove(prayerTime);
        await _context.SaveChangesAsync();

        return true;
    }
}