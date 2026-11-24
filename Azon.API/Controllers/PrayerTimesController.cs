
using Azon.BLL.Interfaces;
using Azon.Shared.DTOs.PrayerTimeDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Azon.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrayerTimesController : ControllerBase
{
    private readonly IPrayerTimeService _prayerTimeService;

    public PrayerTimesController(IPrayerTimeService prayerTimeService)
    {
        _prayerTimeService = prayerTimeService;
    }

    // Ommaviy GET endpointlar
    [AllowAnonymous]
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var prayerTime = await _prayerTimeService.GetByIdAsync(id);

        if (prayerTime is null)
            return NotFound();

        return Ok(prayerTime);
    }

    [AllowAnonymous]
    [HttpGet("city/{cityId:long}")]
    public async Task<IActionResult> GetByCityId(long cityId)
    {
        var prayerTimes = await _prayerTimeService.GetByCityIdAsync(cityId);
        return Ok(prayerTimes);
    }

    [AllowAnonymous]
    [HttpGet("city/{cityId:long}/date/{date}")]
    public async Task<IActionResult> GetByCityAndDate(
        long cityId,
        DateOnly date)
    {
        var prayerTime = await _prayerTimeService
            .GetByCityAndDateAsync(cityId, date);

        if (prayerTime is null)
            return NotFound();

        return Ok(prayerTime);
    }

    // Faqat Admin namoz vaqtini qo‘sha oladi.
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] PrayerTimeForCreateDto dto)
    {
        var prayerTime = await _prayerTimeService.CreateAsync(dto);
        return Ok(prayerTime);
    }

    // Faqat Admin namoz vaqtini yangilaydi.
    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> Update(
        [FromBody] PrayerTimeForUpdateDto dto)
    {
        var prayerTime = await _prayerTimeService.UpdateAsync(dto);

        if (prayerTime is null)
            return NotFound();

        return Ok(prayerTime);
    }

    // Faqat Admin namoz vaqtini o‘chira oladi.
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleted = await _prayerTimeService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
