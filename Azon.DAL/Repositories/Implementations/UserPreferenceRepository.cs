using Azon.DAL.DAL;
using Azon.DAL.Repositories.Interfaces;
using Azon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Azon.DAL.Repositories.Implementations;

public class UserPreferenceRepository : IUserPreferenceRepository
{
    private readonly AppDbContext _context;

    public UserPreferenceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserPreference?> GetByIdAsync(long id)
    {
        return await _context.UserPreferences
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<UserPreference?> GetByUserIdAsync(long userId)
    {
        return await _context.UserPreferences
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == userId);
    }

    public async Task<IEnumerable<UserPreference>> GetAllAsync()
    {
        return await _context.UserPreferences
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<UserPreference> CreateAsync(UserPreference preference)
    {
        await _context.UserPreferences.AddAsync(preference);
        await _context.SaveChangesAsync();

        return preference;
    }

    public async Task<UserPreference> UpdateAsync(UserPreference preference)
    {
        _context.UserPreferences.Update(preference);
        await _context.SaveChangesAsync();

        return preference;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var preference = await _context.UserPreferences.FindAsync(id);

        if (preference is null)
            return false;

        _context.UserPreferences.Remove(preference);
        await _context.SaveChangesAsync();

        return true;
    }
}