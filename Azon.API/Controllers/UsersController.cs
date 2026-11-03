using Azon.BLL.Interfaces;
using Azon.Shared.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Azon.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user is null)
            return NotFound();

        return Ok(user);
    }

    [HttpGet("telegram/{telegramUserId:long}")]
    public async Task<IActionResult> GetByTelegramUserId(long telegramUserId)
    {
        var user = await _userService.GetByTelegramUserIdAsync(telegramUserId);

        if (user is null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserForCreateDto dto)
    {
        var user = await _userService.CreateAsync(dto);
        return Ok(user);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UserForUpdateDto dto)
    {
        var user = await _userService.UpdateAsync(dto);

        if (user is null)
            return NotFound();

        return Ok(user);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleted = await _userService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}