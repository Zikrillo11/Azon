
using Azon.Domain.Common;
using Azon.Domain.Enums;

namespace Azon.Domain.Entities;

public class User : BaseEntity
{
    public long TelegramUserId { get; set; }

    public string? TelegramUsername { get; set; }

    public required string FirstName { get; set; }

    public string? LastName { get; set; }

    public bool IsActive { get; set; } = true;

    public UserRole Role { get; set; } = UserRole.User;

    public DateTime? LastActivityAtUtc { get; set; }

    public UserPreference? Preference { get; set; }

    public string LanguageCode { get; set; } = "uz";

    public ICollection<NotificationLog> NotificationLogs { get; set; }
        = new List<NotificationLog>();
}
