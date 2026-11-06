
namespace Azon.Shared.Settings;

public sealed class TelegramSettings
{
    public string BotToken { get; set; } = string.Empty;

    public string BotUsername { get; set; } = string.Empty;

    public int InitDataExpirationSeconds { get; set; } = 300;
}
