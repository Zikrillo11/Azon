
using Azon.BLL.Interfaces;
using Azon.Shared.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Azon.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly ITelegramAuthService _telegramAuthService;

    public AuthController(
        ITelegramAuthService telegramAuthService)
    {
        _telegramAuthService = telegramAuthService;
    }

    [AllowAnonymous]
    [HttpPost("telegram")]
    [ProducesResponseType(typeof(AuthTokenResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> TelegramLogin(
        [FromBody] TelegramAuthRequestDto request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.InitData))
        {
            return BadRequest(new
            {
                Message = "Telegram InitData is required."
            });
        }

        try
        {
            var result = await _telegramAuthService.AuthenticateAsync(
                request.InitData,
                cancellationToken);

            return Ok(result);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new
            {
                Message = "Telegram authentication failed."
            });
        }
    }
}
