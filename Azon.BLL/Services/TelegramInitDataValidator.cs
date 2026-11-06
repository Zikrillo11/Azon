
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Azon.Shared.Settings;
using Microsoft.Extensions.Options;

namespace Azon.BLL.Services;

public sealed class TelegramInitDataValidator
{
    private readonly TelegramSettings _settings;

    public TelegramInitDataValidator(
        IOptions<TelegramSettings> options)
    {
        _settings = options.Value;
    }

    public bool TryValidate(
        string initData,
        out long telegramUserId)
    {
        telegramUserId = 0;

        if (string.IsNullOrWhiteSpace(initData) ||
            string.IsNullOrWhiteSpace(_settings.BotToken) ||
            _settings.InitDataExpirationSeconds <= 0 ||
            initData.Length > 10_000)
        {
            return false;
        }

        var values = new SortedDictionary<string, string>(
            StringComparer.Ordinal);

        string? receivedHash = null;

        foreach (var item in initData.Split('&'))
        {
            var separatorIndex = item.IndexOf('=');

            if (separatorIndex <= 0)
                return false;

            var key = WebUtility.UrlDecode(
                item[..separatorIndex]);

            var value = WebUtility.UrlDecode(
                item[(separatorIndex + 1)..]);

            if (string.IsNullOrWhiteSpace(key))
                return false;

            if (key == "hash")
            {
                if (receivedHash is not null)
                    return false;

                receivedHash = value;
                continue;
            }

            if (!values.TryAdd(key, value))
                return false;
        }

        if (string.IsNullOrWhiteSpace(receivedHash) ||
            receivedHash.Length != 64 ||
            !receivedHash.All(Uri.IsHexDigit))
        {
            return false;
        }

        if (!values.TryGetValue(
                "auth_date", out var authDateValue) ||
            !long.TryParse(
                authDateValue,
                NumberStyles.None,
                CultureInfo.InvariantCulture,
                out var authDate))
        {
            return false;
        }

        var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        if (authDate <= 0 ||
            authDate > now + 30 ||
            now - authDate > _settings.InitDataExpirationSeconds)
        {
            return false;
        }

        // Telegram data-check-string
        var dataCheckString = string.Join(
            "\n",
            values.Select(x => $"{x.Key}={x.Value}"));

        // Telegram official HMAC-SHA256 algorithm
        var secretKey = HMACSHA256.HashData(
            Encoding.UTF8.GetBytes(_settings.BotToken),
            Encoding.UTF8.GetBytes("WebAppData"));

        var calculatedHash = HMACSHA256.HashData(
            secretKey,
            Encoding.UTF8.GetBytes(dataCheckString));

        byte[] providedHash;

        try
        {
            providedHash = Convert.FromHexString(receivedHash);
        }
        catch (FormatException)
        {
            return false;
        }

        if (!CryptographicOperations.FixedTimeEquals(
                calculatedHash,
                providedHash))
        {
            return false;
        }

        // Parse user only after successful signature validation.
        if (!values.TryGetValue("user", out var userJson))
            return false;

        try
        {
            using var document =
                JsonDocument.Parse(userJson);

            if (!document.RootElement.TryGetProperty(
                    "id", out var idElement))
            {
                return false;
            }

            if (!idElement.TryGetInt64(out telegramUserId) ||
                telegramUserId <= 0)
            {
                telegramUserId = 0;
                return false;
            }

            return true;
        }
        catch (JsonException)
        {
            telegramUserId = 0;
            return false;
        }
    }
}
