using Azon.Domain.Entities;

namespace Azon.DAL.Repositories.Interfaces;

public interface INotificationLogRepository
{
    Task<NotificationLog?> GetByIdAsync(long id);
    Task<IEnumerable<NotificationLog>> GetByUserIdAsync(long userId);
    Task<IEnumerable<NotificationLog>> GetPendingNotificationsAsync(DateOnly date);
    Task<NotificationLog> CreateAsync(NotificationLog notificationLog);
    Task<NotificationLog> UpdateAsync(NotificationLog notificationLog);
    Task<bool> DeleteAsync(long id);
}