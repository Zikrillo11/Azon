using Azon.Domain.Entities;

namespace Azon.DAL.Repositories.Interfaces;

public interface IUserPreferenceRepository
{
    Task<UserPreference?> GetByIdAsync(long id);
    Task<UserPreference?> GetByUserIdAsync(long userId);
    Task<IEnumerable<UserPreference>> GetAllAsync();
    Task<UserPreference> CreateAsync(UserPreference preference);
    Task<UserPreference> UpdateAsync(UserPreference preference);
    Task<bool> DeleteAsync(long id);
}