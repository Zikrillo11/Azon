using Azon.BLL.Interfaces;
using Azon.Shared.DTOs.PrayerTimeDTOs;
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

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var prayerTime = await _prayerTimeService.GetByIdAsync(id);

        if (prayerTime is null)
            return NotFound();

        return Ok(prayerTime);
    }

    [HttpGet("city/{cityId:long}")]
    public async Task<IActionResult> GetByCityId(long cityId)
    {
        var prayerTimes = await _prayerTimeService.GetByCityIdAsync(cityId);
        return Ok(prayerTimes);
    }

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

    [HttpPost]
    public async Task<IActionResult> Create(PrayerTimeForCreateDto dto)
    {
        var prayerTime = await _prayerTimeService.CreateAsync(dto);
        return Ok(prayerTime);
    }

    [HttpPut]
    public async Task<IActionResult> Update(PrayerTimeForUpdateDto dto)
    {
        var prayerTime = await _prayerTimeService.UpdateAsync(dto);

        if (prayerTime is null)
            return NotFound();

        return Ok(prayerTime);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleted = await _prayerTimeService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}