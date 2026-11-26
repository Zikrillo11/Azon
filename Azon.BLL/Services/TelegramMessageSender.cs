
using Azon.BLL.Interfaces;
using Azon.Shared.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

namespace Azon.BLL.Services;

public sealed class TelegramMessageSender : ITelegramMessageSender
{
    private readonly HttpClient _httpClient;
    private readonly TelegramSettings _settings;

    public TelegramMessageSender(
        HttpClient httpClient,
        IOptions<TelegramSettings> options)
    {
        _httpClient = httpClient;
        _settings = options.Value;
    }

    public async Task SendMessageAsync(
        long chatId,
        string message,
        CancellationToken cancellationToken = default)
    {
        if (chatId <= 0)
            throw new ArgumentException("Invalid Telegram chat ID.");

        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("Message cannot be empty.");

        if (message.Length > 4096)
            throw new ArgumentException("Telegram message is too long.");

        if (string.IsNullOrWhiteSpace(_settings.BotToken))
            throw new InvalidOperationException(
                "Telegram BotToken is not configured.");

        var url =
            $"https://api.telegram.org/bot{_settings.BotToken}/sendMessage";

        var payload = new
        {
            chat_id = chatId,
            text = message
        };

        using var response = await _httpClient.PostAsJsonAsync(
            url,
            payload,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        await using var stream =
            await response.Content.ReadAsStreamAsync(cancellationToken);

        using var document = await JsonDocument.ParseAsync(
            stream,
            cancellationToken: cancellationToken);

        var root = document.RootElement;

        if (!root.TryGetProperty("ok", out var ok) ||
            !ok.GetBoolean())
        {
            var description = root.TryGetProperty(
                "description", out var error)
                ? error.GetString()
                : "Unknown Telegram API error.";

            throw new InvalidOperationException(
                $"Telegram message sending failed: {description}");
        }
    }
}
