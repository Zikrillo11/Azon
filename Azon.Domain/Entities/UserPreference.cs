
using Azon.Domain.Common;

namespace Azon.Domain.Entities;

public class UserPreference : BaseEntity
{
    public long UserId { get; set; }

    public long CityId { get; set; }

    public bool NotificationsEnabled { get; set; } = true;

    public bool FajrNotificationEnabled { get; set; } = true;

    public bool DhuhrNotificationEnabled { get; set; } = true;

    public bool AsrNotificationEnabled { get; set; } = true;

    public bool MaghribNotificationEnabled { get; set; } = true;

    public bool IshaNotificationEnabled { get; set; } = true;

    public User User { get; set; } = null!;

    public City City { get; set; } = null!;
}
