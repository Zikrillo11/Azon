
using Azon.Domain.Common;
using Azon.Domain.Enums;

namespace Azon.Domain.Entities;

public class NotificationLog : BaseEntity
{
    public long UserId { get; set; }
    public DateOnly PrayerDate { get; set; }
    public PrayerType PrayerType { get; set; }
    public NotificationStatus Status { get; set; }
        = NotificationStatus.Pending;
    public DateTime? SentAtUtc { get; set; }
    public string? ErrorMessage { get; set; }
    public User User { get; set; } = null!;
}
