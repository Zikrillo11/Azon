using Azon.BLL.Interfaces;
using Azon.Shared.DTOs.MosqueDTOs;
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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var mosques = await _mosqueService.GetAllAsync();
        return Ok(mosques);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var mosque = await _mosqueService.GetByIdAsync(id);

        if (mosque is null)
            return NotFound();

        return Ok(mosque);
    }

    [HttpGet("city/{cityId:long}")]
    public async Task<IActionResult> GetByCityId(long cityId)
    {
        var mosques = await _mosqueService.GetByCityIdAsync(cityId);
        return Ok(mosques);
    }

    [HttpPost]
    public async Task<IActionResult> Create(MosqueForCreateDto dto)
    {
        var mosque = await _mosqueService.CreateAsync(dto);
        return Ok(mosque);
    }

    [HttpPut]
    public async Task<IActionResult> Update(MosqueForUpdateDto dto)
    {
        var mosque = await _mosqueService.UpdateAsync(dto);

        if (mosque is null)
            return NotFound();

        return Ok(mosque);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleted = await _mosqueService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}