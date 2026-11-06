
using Azon.BLL.Interfaces;
using Azon.DAL.Repositories.Interfaces;
using Azon.Shared.DTOs.AuthDTOs;
using Azon.Shared.Settings;
using Microsoft.Extensions.Options;

namespace Azon.BLL.Services;

public sealed class TelegramAuthService : ITelegramAuthService
{
    private readonly TelegramInitDataValidator _validator;
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly JwtSettings _jwtSettings;

    public TelegramAuthService(
        TelegramInitDataValidator validator,
        IUserRepository userRepository,
        IJwtService jwtService,
        IOptions<JwtSettings> jwtOptions)
    {
        _validator = validator;
        _userRepository = userRepository;
        _jwtService = jwtService;
        _jwtSettings = jwtOptions.Value;
    }

    public async Task<AuthTokenResultDto> AuthenticateAsync(
        string initData,
        CancellationToken cancellationToken = default)
    {
        if (!_validator.TryValidate(initData, out var telegramUserId))
        {
            throw new UnauthorizedAccessException(
                "Invalid Telegram authentication data.");
        }

        var user = await _userRepository
            .GetByTelegramUserIdAsync(telegramUserId);

        if (user is null)
        {
            throw new UnauthorizedAccessException(
                "User is not registered.");
        }

        if (!user.IsActive)
        {
            throw new UnauthorizedAccessException(
                "User account is inactive.");
        }

        var accessToken = _jwtService.GenerateAccessToken(user);

        return new AuthTokenResultDto
        {
            AccessToken = accessToken,
            TokenType = "Bearer",
            ExpiresInSeconds =
                _jwtSettings.AccessTokenExpirationMinutes * 60
        };
    }
}
