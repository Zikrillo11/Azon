using Azon.Shared.DTOs.NotificationLogDTOs;

namespace Azon.BLL.Interfaces;

public interface INotificationLogService
{
    Task<NotificationLogForResultDto?> GetByIdAsync(long id);

    Task<IEnumerable<NotificationLogForResultDto>> GetByUserIdAsync(
        long userId);

    Task<IEnumerable<NotificationLogForShortResultDto>> GetPendingNotificationsAsync(
        DateOnly date);

    Task<NotificationLogForResultDto> CreateAsync(
        NotificationLogForCreateDto dto);
     
    Task<NotificationLogForResultDto?> UpdateAsync(
        NotificationLogForUpdateDto dto);

    Task<bool> DeleteAsync(long id);
}