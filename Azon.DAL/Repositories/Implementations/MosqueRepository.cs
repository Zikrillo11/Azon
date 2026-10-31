using Azon.DAL.DAL;
using Azon.DAL.Repositories.Interfaces;
using Azon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Azon.DAL.Repositories.Implementations;

public class MosqueRepository : IMosqueRepository
{
    private readonly AppDbContext _context;

    public MosqueRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Mosque?> GetByIdAsync(long id)
    {
        return await _context.Mosques
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Mosque>> GetAllAsync()
    {
        return await _context.Mosques
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Mosque>> GetByCityIdAsync(long cityId)
    {
        return await _context.Mosques
            .AsNoTracking()
            .Where(x => x.CityId == cityId && x.IsActive)
            .ToListAsync();
    }

    public async Task<Mosque> CreateAsync(Mosque mosque)
    {
        await _context.Mosques.AddAsync(mosque);
        await _context.SaveChangesAsync();

        return mosque;
    }

    public async Task<Mosque> UpdateAsync(Mosque mosque)
    {
        _context.Mosques.Update(mosque);
        await _context.SaveChangesAsync();

        return mosque;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var mosque = await _context.Mosques.FindAsync(id);

        if (mosque is null)
            return false;

        _context.Mosques.Remove(mosque);
        await _context.SaveChangesAsync();

        return true;
    }
}