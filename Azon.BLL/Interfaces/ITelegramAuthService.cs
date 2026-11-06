
using Azon.Shared.DTOs.AuthDTOs;

namespace Azon.BLL.Interfaces;

public interface ITelegramAuthService
{
    Task<AuthTokenResultDto> AuthenticateAsync(
        string initData,
        CancellationToken cancellationToken = default);
}
