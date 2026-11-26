
using Azon.Shared.Settings;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Azon.BLL.Services;

public sealed record TelegramUserData(
    long Id,
    string FirstName,
    string? LastName,
    string? Username,
    string? LanguageCode);

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
        out TelegramUserData? telegramUser)
    {
        telegramUser = null;

        if (string.IsNullOrWhiteSpace(initData) ||
            string.IsNullOrWhiteSpace(_settings.BotToken))
            return false;

        try
        {
            var values = new SortedDictionary<string, string>(
                StringComparer.Ordinal);

            foreach (var item in initData.Split('&'))
            {
                var separator = item.IndexOf('=');

                if (separator <= 0)
                    return false;

                var key = WebUtility.UrlDecode(
                    item[..separator]);

                var value = WebUtility.UrlDecode(
                    item[(separator + 1)..]);

                if (string.IsNullOrWhiteSpace(key) ||
                    !values.TryAdd(key, value))
                    return false;
            }

            if (!values.TryGetValue("hash", out var receivedHash) ||
                receivedHash.Length != 64 ||
                !values.TryGetValue("auth_date", out var authDateText) ||
                !values.TryGetValue("user", out var userJson))
                return false;

            var dataCheckString = string.Join(
                "\n",
                values
                    .Where(x => x.Key != "hash")
                    .Select(x => $"{x.Key}={x.Value}"));

            var secretKey = HMACSHA256.HashData(
                Encoding.UTF8.GetBytes(_settings.BotToken),
                Encoding.UTF8.GetBytes("WebAppData"));

            var calculatedHash = HMACSHA256.HashData(
                secretKey,
                Encoding.UTF8.GetBytes(dataCheckString));

            var receivedHashBytes =
                Convert.FromHexString(receivedHash);

            if (!CryptographicOperations.FixedTimeEquals(
                    calculatedHash,
                    receivedHashBytes))
                return false;

            if (!long.TryParse(
                    authDateText,
                    NumberStyles.Integer,
                    CultureInfo.InvariantCulture,
                    out var authTimestamp))
                return false;

            var authDate = DateTimeOffset.FromUnixTimeSeconds(
                authTimestamp);

            var now = DateTimeOffset.UtcNow;

            if (authDate > now.AddSeconds(30))
                return false;

            if ((now - authDate).TotalSeconds >
                _settings.InitDataExpirationSeconds)
                return false;

            using var document = JsonDocument.Parse(userJson);

            var root = document.RootElement;

            if (!root.TryGetProperty("id", out var idElement) ||
                !idElement.TryGetInt64(out var telegramId) ||
                telegramId <= 0)
                return false;

            if (!root.TryGetProperty(
                    "first_name", out var firstNameElement))
                return false;

            var firstName = firstNameElement.GetString();

            if (string.IsNullOrWhiteSpace(firstName))
                return false;

            var lastName = root.TryGetProperty(
                "last_name", out var lastNameElement)
                ? lastNameElement.GetString()
                : null;

            var username = root.TryGetProperty(
                "username", out var usernameElement)
                ? usernameElement.GetString()
                : null;

            var languageCode = root.TryGetProperty(
                "language_code", out var languageElement)
                ? languageElement.GetString()
                : null;

            telegramUser = new TelegramUserData(
                telegramId,
                firstName,
                lastName,
                username,
                languageCode);

            return true;
        }
        catch
        {
            telegramUser = null;
            return false;
        }
    }
}
