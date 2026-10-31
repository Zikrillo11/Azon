using Azon.Shared.DTOs.MosqueDTOs;

namespace Azon.BLL.Interfaces;

public interface IMosqueService
{
    Task<MosqueForResultDto?> GetByIdAsync(long id);

    Task<IEnumerable<MosqueForResultDto>> GetAllAsync();

    Task<IEnumerable<MosqueForShortResultDto>> GetByCityIdAsync(long cityId);

    Task<MosqueForResultDto> CreateAsync(MosqueForCreateDto dto);

    Task<MosqueForResultDto?> UpdateAsync(MosqueForUpdateDto dto);

    Task<bool> DeleteAsync(long id);
}