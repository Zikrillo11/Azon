using Azon.Domain.Entities;

namespace Azon.DAL.Repositories.Interfaces;

public interface IMosqueRepository
{
    Task<Mosque?> GetByIdAsync(long id);
    Task<IEnumerable<Mosque>> GetAllAsync();
    Task<IEnumerable<Mosque>> GetByCityIdAsync(long cityId);
    Task<Mosque> CreateAsync(Mosque mosque);
    Task<Mosque> UpdateAsync(Mosque mosque);
    Task<bool> DeleteAsync(long id);
}