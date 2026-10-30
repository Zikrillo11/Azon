using Azon.Domain.Enums;

namespace Azon.Shared.DTOs.NotificationLogDTOs;

public class NotificationLogForCreateDto
{
    public long UserId { get; set; }
    public DateOnly PrayerDate { get; set; }
    public PrayerType PrayerType { get; set; }
}