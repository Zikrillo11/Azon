
using Azon.BLL.Interfaces;
using Azon.Shared.DTOs.CityDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Azon.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    private readonly ICityService _cityService;

    public CitiesController(ICityService cityService)
    {
        _cityService = cityService;
    }

    // Barcha foydalanuvchilar shaharlarni ko‘ra oladi.
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cities = await _cityService.GetAllAsync();
        return Ok(cities);
    }

    [AllowAnonymous]
    [HttpGet("active")]
    public async Task<IActionResult> GetActiveCities()
    {
        var cities = await _cityService.GetActiveCitiesAsync();
        return Ok(cities);
    }

    [AllowAnonymous]
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var city = await _cityService.GetByIdAsync(id);

        if (city is null)
            return NotFound();

        return Ok(city);
    }

    // Faqat Admin shahar qo‘sha oladi.
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CityForCreateDto dto)
    {
        var city = await _cityService.CreateAsync(dto);
        return Ok(city);
    }

    // Faqat Admin shaharni yangilay oladi.
    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> Update(
        [FromBody] CityForUpdateDto dto)
    {
        var city = await _cityService.UpdateAsync(dto);

        if (city is null)
            return NotFound();

        return Ok(city);
    }

    // Faqat Admin shaharni o‘chira oladi.
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleted = await _cityService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
