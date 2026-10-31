using Azon.Domain.Entities;

namespace Azon.DAL.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(long id);
    Task<User?> GetByTelegramUserIdAsync(long telegramUserId);
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> CreateAsync(User user);
    Task<User> UpdateAsync(User user);
    Task<bool> DeleteAsync(long id);
}