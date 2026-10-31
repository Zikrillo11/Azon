
using Azon.Domain.Common;

namespace Azon.Domain.Entities;

public class User : BaseEntity
{
    public long TelegramUserId { get; set; }
    public string? TelegramUsername { get; set; }
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? LastActivityAtUtc { get; set; }
    public UserPreference? Preference { get; set; }
    public string LanguageCode { get; set; } = "uz";
    public ICollection<NotificationLog> NotificationLogs { get; set; }
        = new List<NotificationLog>();
}
