namespace Azon.Shared.DTOs.UserDTOs;

public class UserForUpdateDto
{
    public long Id { get; set; }
    public string? TelegramUsername { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? LanguageCode { get; set; }
    public bool IsActive { get; set; }
}