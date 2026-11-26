
using Azon.BLL.Interfaces;
using Azon.DAL.Repositories.Interfaces;
using Azon.Domain.Entities;
using Azon.Domain.Enums;
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
        if (!_validator.TryValidate(
                initData, out var telegramUser) ||
            telegramUser is null)
        {
            throw new UnauthorizedAccessException(
                "Invalid Telegram authentication data.");
        }

        var user = await _userRepository
            .GetByTelegramUserIdAsync(telegramUser.Id);

        if (user is null)
        {
            user = new User
            {
                TelegramUserId = telegramUser.Id,
                TelegramUsername = telegramUser.Username,
                FirstName = telegramUser.FirstName,
                LastName = telegramUser.LastName,
                LanguageCode = string.IsNullOrWhiteSpace(
                    telegramUser.LanguageCode)
                    ? "uz"
                    : telegramUser.LanguageCode,
                IsActive = true,
                Role = UserRole.User,
                LastActivityAtUtc = DateTime.UtcNow
            };

            await _userRepository.CreateAsync(user);
        }
        else
        {
            if (!user.IsActive)
            {
                throw new UnauthorizedAccessException(
                    "User account is inactive.");
            }

            user.TelegramUsername = telegramUser.Username;
            user.FirstName = telegramUser.FirstName;
            user.LastName = telegramUser.LastName;

            user.LanguageCode = string.IsNullOrWhiteSpace(
                telegramUser.LanguageCode)
                ? user.LanguageCode
                : telegramUser.LanguageCode;

            user.LastActivityAtUtc = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
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
