
using Azon.BLL.Interfaces;
using Azon.Shared.DTOs.UserPreferenceDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Azon.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserPreferencesController : ControllerBase
{
    private readonly IUserPreferenceService _userPreferenceService;

    public UserPreferencesController(
        IUserPreferenceService userPreferenceService)
    {
        _userPreferenceService = userPreferenceService;
    }

    private bool IsAdmin => User.IsInRole("Admin");

    private long CurrentUserId =>
        long.TryParse(
            User.FindFirstValue(ClaimTypes.NameIdentifier),
            out var userId)
                ? userId
                : 0;

    // Barcha sozlamalarni faqat Admin ko‘radi.
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var preferences = await _userPreferenceService.GetAllAsync();
        return Ok(preferences);
    }

    // Foydalanuvchi faqat o‘z sozlamasini ko‘radi.
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var preference = await _userPreferenceService.GetByIdAsync(id);

        if (preference is null)
            return NotFound();

        if (!IsAdmin && preference.UserId != CurrentUserId)
            return Forbid();

        return Ok(preference);
    }

    [HttpGet("user/{userId:long}")]
    public async Task<IActionResult> GetByUserId(long userId)
    {
        if (!IsAdmin && userId != CurrentUserId)
            return Forbid();

        var preference = await _userPreferenceService
            .GetByUserIdAsync(userId);

        if (preference is null)
            return NotFound();

        return Ok(preference);
    }

    // UserId token orqali olinadi.
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] UserPreferenceForCreateDto dto)
    {
        if (!IsAdmin)
            dto.UserId = CurrentUserId;

        if (dto.UserId <= 0)
            return Unauthorized();

        var preference = await _userPreferenceService.CreateAsync(dto);
        return Ok(preference);
    }

    // Faqat sozlama egasi yoki Admin yangilaydi.
    [HttpPut]
    public async Task<IActionResult> Update(
        [FromBody] UserPreferenceForUpdateDto dto)
    {
        var existing = await _userPreferenceService.GetByIdAsync(dto.Id);

        if (existing is null)
            return NotFound();

        if (!IsAdmin && existing.UserId != CurrentUserId)
            return Forbid();

        var preference = await _userPreferenceService.UpdateAsync(dto);

        if (preference is null)
            return NotFound();

        return Ok(preference);
    }

    // Faqat sozlama egasi yoki Admin o‘chiradi.
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var existing = await _userPreferenceService.GetByIdAsync(id);

        if (existing is null)
            return NotFound();

        if (!IsAdmin && existing.UserId != CurrentUserId)
            return Forbid();

        var deleted = await _userPreferenceService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
