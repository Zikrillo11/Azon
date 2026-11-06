
namespace Azon.Shared.DTOs.AuthDTOs;

public sealed class AuthTokenResultDto
{
    public string AccessToken { get; set; } = string.Empty;

    public string TokenType { get; set; } = "Bearer";

    public int ExpiresInSeconds { get; set; }
}
