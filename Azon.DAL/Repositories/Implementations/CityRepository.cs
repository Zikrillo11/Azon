using Azon.DAL.DAL;
using Azon.DAL.Repositories.Interfaces;
using Azon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Azon.DAL.Repositories.Implementations;

public class CityRepository : ICityRepository
{
    private readonly AppDbContext _context;

    public CityRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<City?> GetByIdAsync(long id)
    {
        return await _context.Cities
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<City>> GetAllAsync()
    {
        return await _context.Cities
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<City>> GetActiveCitiesAsync()
    {
        return await _context.Cities
            .AsNoTracking()
            .Where(x => x.IsActive)
            .ToListAsync();
    }

    public async Task<City> CreateAsync(City city)
    {
        await _context.Cities.AddAsync(city);
        await _context.SaveChangesAsync();

        return city;
    }

    public async Task<City> UpdateAsync(City city)
    {
        _context.Cities.Update(city);
        await _context.SaveChangesAsync();

        return city;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var city = await _context.Cities.FindAsync(id);

        if (city is null)
            return false;

        _context.Cities.Remove(city);
        await _context.SaveChangesAsync();

        return true;
    }
}