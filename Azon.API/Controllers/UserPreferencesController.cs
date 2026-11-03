using Azon.BLL.Interfaces;
using Azon.Shared.DTOs.UserPreferenceDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Azon.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserPreferencesController : ControllerBase
{
    private readonly IUserPreferenceService _userPreferenceService;

    public UserPreferencesController(
        IUserPreferenceService userPreferenceService)
    {
        _userPreferenceService = userPreferenceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var preferences = await _userPreferenceService.GetAllAsync();
        return Ok(preferences);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var preference = await _userPreferenceService.GetByIdAsync(id);

        if (preference is null)
            return NotFound();

        return Ok(preference);
    }

    [HttpGet("user/{userId:long}")]
    public async Task<IActionResult> GetByUserId(long userId)
    {
        var preference = await _userPreferenceService.GetByUserIdAsync(userId);

        if (preference is null)
            return NotFound();

        return Ok(preference);
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserPreferenceForCreateDto dto)
    {
        var preference = await _userPreferenceService.CreateAsync(dto);
        return Ok(preference);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UserPreferenceForUpdateDto dto)
    {
        var preference = await _userPreferenceService.UpdateAsync(dto);

        if (preference is null)
            return NotFound();

        return Ok(preference);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleted = await _userPreferenceService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}