using Azon.Shared.DTOs.CityDTOs;

namespace Azon.BLL.Interfaces;

public interface ICityService
{
    Task<CityForResultDto?> GetByIdAsync(long id);

    Task<IEnumerable<CityForResultDto>> GetAllAsync();

    Task<IEnumerable<CityForShortResultDto>> GetActiveCitiesAsync();

    Task<CityForResultDto> CreateAsync(CityForCreateDto dto);

    Task<CityForResultDto?> UpdateAsync(CityForUpdateDto dto);

    Task<bool> DeleteAsync(long id);
}