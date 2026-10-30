using Azon.Domain.Enums;

namespace Azon.Shared.DTOs.NotificationLogDTOs;

public class NotificationLogForUpdateDto
{
    public long Id { get; set; }
    public NotificationStatus NotificationStatus { get; set; }
    public DateTime? SentAtUtc { get; set; }
    public string? ErrorMessage { get; set; }
}