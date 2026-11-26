namespace Azon.BLL.Interfaces;

public interface ITelegramMessageSender
{
    Task SendMessageAsync(
        long chatId,
        string message,
        CancellationToken cancellationToken = default);
}