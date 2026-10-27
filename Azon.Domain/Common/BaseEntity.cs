
namespace Azon.Domain.Common;

public abstract class BaseEntity
{
    public long Id { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAtUtc { get; set; }
}
