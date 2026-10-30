namespace Azon.Shared.DTOs.UserDTOs;

public class UserForShortResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string LanguageCode { get; set; } = "uz";
}