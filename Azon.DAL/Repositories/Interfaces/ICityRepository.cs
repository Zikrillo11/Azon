using Azon.Domain.Entities;

namespace Azon.DAL.Repositories.Interfaces;

public interface ICityRepository
{
    Task<City?> GetByIdAsync(long id);
    Task<IEnumerable<City>> GetAllAsync();
    Task<IEnumerable<City>> GetActiveCitiesAsync();
    Task<City> CreateAsync(City city);
    Task<City> UpdateAsync(City city);
    Task<bool> DeleteAsync(long id);
}