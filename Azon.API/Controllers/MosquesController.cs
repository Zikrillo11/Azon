
using Azon.BLL.Interfaces;
using Azon.Shared.DTOs.MosqueDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Azon.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MosquesController : ControllerBase
{
    private readonly IMosqueService _mosqueService;

    public MosquesController(IMosqueService mosqueService)
    {
        _mosqueService = mosqueService;
    }

    // Ommaviy GET endpointlar
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var mosques = await _mosqueService.GetAllAsync();
        return Ok(mosques);
    }

    [AllowAnonymous]
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var mosque = await _mosqueService.GetByIdAsync(id);

        if (mosque is null)
            return NotFound();

        return Ok(mosque);
    }

    [AllowAnonymous]
    [HttpGet("city/{cityId:long}")]
    public async Task<IActionResult> GetByCityId(long cityId)
    {
        var mosques = await _mosqueService.GetByCityIdAsync(cityId);
        return Ok(mosques);
    }

    // Faqat Admin masjid qo‘sha oladi.
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] MosqueForCreateDto dto)
    {
        var mosque = await _mosqueService.CreateAsync(dto);
        return Ok(mosque);
    }

    // Faqat Admin masjid ma'lumotlarini yangilaydi.
    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> Update(
        [FromBody] MosqueForUpdateDto dto)
    {
        var mosque = await _mosqueService.UpdateAsync(dto);

        if (mosque is null)
            return NotFound();

        return Ok(mosque);
    }

    // Faqat Admin masjidni o‘chira oladi.
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleted = await _mosqueService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
