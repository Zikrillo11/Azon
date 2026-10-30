using Azon.Domain.Enums;

namespace Azon.Shared.DTOs.NotificationLogDTOs;

public class NotificationLogForResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string? UserName { get; set; }
    public DateOnly PrayerDate { get; set; }
    public PrayerType PrayerType { get; set; }
    public NotificationStatus NotificationStatus { get; set; }
    public DateTime? SentAtUtc { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}