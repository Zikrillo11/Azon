using Azon.Shared.DTOs.UserDTOs;

namespace Azon.BLL.Interfaces;

public interface IUserService
{
    Task<UserForResultDto?> GetByIdAsync(long id);

    Task<UserForResultDto?> GetByTelegramUserIdAsync(long telegramUserId);

    Task<IEnumerable<UserForResultDto>> GetAllAsync();

    Task<UserForResultDto> CreateAsync(UserForCreateDto dto);

    Task<UserForResultDto?> UpdateAsync(UserForUpdateDto dto);

    Task<bool> DeleteAsync(long id);
}