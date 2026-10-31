namespace Azon.Shared.DTOs.UserPreferenceDTOs;

public class PrayerTimeForShortResultDto
{
    public long UserId { get; set; }
    public long CityId { get; set; }
    public bool NotificationsEnabled { get; set; } = true;
    public bool FajrNotificationEnabled { get; set; } = true;
    public bool DhuhrNotificationEnabled { get; set; } = true;
    public bool AsrNotificationEnabled { get; set; } = true;
    public bool MaghribNotificationEnabled { get; set; } = true;
    public bool IshaNotificationEnabled { get; set; } = true;
}