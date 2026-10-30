namespace Azon.Shared.DTOs.UserPreferenceDTOs;

public class UserPreferenceForResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long CityId { get; set; }
    public string? CityName { get; set; }
    public bool NotificationsEnabled { get; set; }
    public bool FajrNotificationEnabled { get; set; }
    public bool DhuhrNotificationEnabled { get; set; }
    public bool AsrNotificationEnabled { get; set; }
    public bool MaghribNotificationEnabled { get; set; }
    public bool IshaNotificationEnabled { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}