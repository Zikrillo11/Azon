
using Azon.BLL.Interfaces;
using Azon.Shared.DTOs.NotificationLogDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Azon.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotificationLogsController : ControllerBase
{
    private readonly INotificationLogService _notificationLogService;

    public NotificationLogsController(
        INotificationLogService notificationLogService)
    {
        _notificationLogService = notificationLogService;
    }

    private bool IsAdmin => User.IsInRole("Admin");

    private long CurrentUserId =>
        long.TryParse(
            User.FindFirstValue(ClaimTypes.NameIdentifier),
            out var userId)
                ? userId
                : 0;

    // Faqat log egasi yoki Admin ko‘ra oladi.
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var log = await _notificationLogService.GetByIdAsync(id);

        if (log is null)
            return NotFound();

        if (!IsAdmin && log.UserId != CurrentUserId)
            return Forbid();

        return Ok(log);
    }

    // Foydalanuvchi faqat o‘z loglarini ko‘radi.
    [HttpGet("user/{userId:long}")]
    public async Task<IActionResult> GetByUserId(long userId)
    {
        if (!IsAdmin && userId != CurrentUserId)
            return Forbid();

        var logs = await _notificationLogService.GetByUserIdAsync(userId);
        return Ok(logs);
    }

    // Barcha pending bildirishnomalarni faqat Admin ko‘radi.
    [Authorize(Roles = "Admin")]
    [HttpGet("pending/{date}")]
    public async Task<IActionResult> GetPendingNotifications(DateOnly date)
    {
        var notifications = await _notificationLogService
            .GetPendingNotificationsAsync(date);

        return Ok(notifications);
    }

    // Log yaratish hozircha faqat Admin uchun.
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] NotificationLogForCreateDto dto)
    {
        var log = await _notificationLogService.CreateAsync(dto);
        return Ok(log);
    }

    // Logni faqat Admin yangilaydi.
    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> Update(
        [FromBody] NotificationLogForUpdateDto dto)
    {
        var log = await _notificationLogService.UpdateAsync(dto);

        if (log is null)
            return NotFound();

        return Ok(log);
    }

    // Logni faqat Admin o‘chiradi.
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleted = await _notificationLogService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
