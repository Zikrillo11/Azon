
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Azon.Shared.Settings;

namespace Azon.BLL.Services;

public sealed class TelegramInitDataValidator
{
    private readonly TelegramSettings _settings;

    public TelegramInitDataValidator(
        Microsoft.Extensions.Options.IOptions<TelegramSettings> options)
    {
        _settings = options.Value;
    }

    public bool TryValidate(
        string initData,
        out long telegramUserId)
    {
        telegramUserId = 0;

        if (string.IsNullOrWhiteSpace(initData) ||
            string.IsNullOrWhiteSpace(_settings.BotToken))
        {
            return false;
        }

        if (initData.Length > 10_000)
            return false;

        var values = new SortedDictionary<string, string>(
            StringComparer.Ordinal);

        foreach (var item in initData.Split('&'))
        {
            var separatorIndex = item.IndexOf('=');

            if (separatorIndex <= 0)
                return false;

            var key = Uri.UnescapeDataString(
                item[..separatorIndex]);

            var value = Uri.UnescapeDataString(
                item[(separatorIndex + 1)..]);

            if (key == "hash" || !values.TryAdd(key, value))
            {
                if (key != "hash")
                    return false;
            }
        }

        var hashValues = initData.Split('&')
            .Where(x => x.StartsWith("hash=", StringComparison.Ordinal))
            .ToArray();

        if (hashValues.Length != 1)
            return false;

        var receivedHash = Uri.UnescapeDataString(
            hashValues[0][5..]);

        if (receivedHash.Length != 64 ||
            !receivedHash.All(Uri.IsHexDigit))
            return false;

        if (!values.TryGetValue("user", out var userJson))
            return false;

        try
        {
            using var document =
                System.Text.Json.JsonDocument.Parse(userJson);

            if (!document.RootElement.TryGetProperty(
                    "id", out var idElement))
                return false;

            if (!idElement.TryGetInt64(out telegramUserId))
                return false;

            if (telegramUserId <= 0)
                return false;
        }
        catch (System.Text.Json.JsonException)
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

        // Reject future timestamps and data older than 5 minutes.
        if (authDate > now + 30 ||
            now - authDate > 300)
        {
            return false;
        }

        var dataCheckString = string.Join(
            "\n",
            values.Select(x => $"{x.Key}={x.Value}"));

        var secretKey = HMACSHA256.HashData(
            Encoding.UTF8.GetBytes("WebAppData"),
            Encoding.UTF8.GetBytes(_settings.BotToken));

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

        return CryptographicOperations.FixedTimeEquals(
            calculatedHash,
            providedHash);
    }
}
