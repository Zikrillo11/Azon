using Azon.BLL.Interfaces;
using Azon.Shared.DTOs.NotificationLogDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Azon.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationLogsController : ControllerBase
{
    private readonly INotificationLogService _notificationLogService;

    public NotificationLogsController(
        INotificationLogService notificationLogService)
    {
        _notificationLogService = notificationLogService;
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var log = await _notificationLogService.GetByIdAsync(id);

        if (log is null)
            return NotFound();

        return Ok(log);
    }

    [HttpGet("user/{userId:long}")]
    public async Task<IActionResult> GetByUserId(long userId)
    {
        var logs = await _notificationLogService.GetByUserIdAsync(userId);
        return Ok(logs);
    }

    [HttpGet("pending/{date}")]
    public async Task<IActionResult> GetPendingNotifications(DateOnly date)
    {
        var notifications = await _notificationLogService
            .GetPendingNotificationsAsync(date);

        return Ok(notifications);
    }

    [HttpPost]
    public async Task<IActionResult> Create(NotificationLogForCreateDto dto)
    {
        var log = await _notificationLogService.CreateAsync(dto);
        return Ok(log);
    }

    [HttpPut]
    public async Task<IActionResult> Update(NotificationLogForUpdateDto dto)
    {
        var log = await _notificationLogService.UpdateAsync(dto);

        if (log is null)
            return NotFound();

        return Ok(log);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleted = await _notificationLogService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}