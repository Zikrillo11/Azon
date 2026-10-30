namespace Azon.Shared.DTOs.UserDTOs;

public class UserForResultDto
{
    public long Id { get; set; }
    public long TelegramUserId { get; set; }
    public string? TelegramUsername { get; set; }
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string LanguageCode { get; set; } = "uz";
    public bool IsActive { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}