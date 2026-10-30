using Azon.Domain.Enums;

namespace Azon.Shared.DTOs.NotificationLogDTOs;

public class NotificationLogForShortResultDto
{
    public long Id { get; set; }
    public DateOnly PrayerDate { get; set; }
    public PrayerType PrayerType { get; set; }
    public NotificationStatus NotificationStatus { get; set; }
}