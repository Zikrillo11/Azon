using Azon.BLL.Interfaces;
using Azon.Shared.DTOs.CityDTOs;
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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cities = await _cityService.GetAllAsync();
        return Ok(cities);
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActiveCities()
    {
        var cities = await _cityService.GetActiveCitiesAsync();
        return Ok(cities);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var city = await _cityService.GetByIdAsync(id);

        if (city is null)
            return NotFound();

        return Ok(city);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CityForCreateDto dto)
    {
        var city = await _cityService.CreateAsync(dto);
        return Ok(city);
    }

    [HttpPut]
    public async Task<IActionResult> Update(CityForUpdateDto dto)
    {
        var city = await _cityService.UpdateAsync(dto);

        if (city is null)
            return NotFound();

        return Ok(city);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleted = await _cityService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}