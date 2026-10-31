using Azon.Shared.DTOs.UserPreferenceDTOs;

namespace Azon.BLL.Interfaces;

public interface IUserPreferenceService
{
    Task<UserPreferenceForResultDto?> GetByIdAsync(long id);

    Task<UserPreferenceForResultDto?> GetByUserIdAsync(long userId);

    Task<IEnumerable<UserPreferenceForResultDto>> GetAllAsync();

    Task<UserPreferenceForResultDto> CreateAsync(
        UserPreferenceForCreateDto dto);

    Task<UserPreferenceForResultDto?> UpdateAsync(
        UserPreferenceForUpdateDto dto);

    Task<bool> DeleteAsync(long id);
}