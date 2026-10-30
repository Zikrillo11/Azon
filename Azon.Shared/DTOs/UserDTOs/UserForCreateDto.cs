namespace Azon.Shared.DTOs.UserDTOs;

public class UserForCreateDto
{
    public long TelegramUserId { get; set; }
    public string? TelegramUsername { get; set; }
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public string LanguageCode { get; set; } = "uz";
}