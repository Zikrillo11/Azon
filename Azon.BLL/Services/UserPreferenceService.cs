
using AutoMapper;
using Azon.BLL.Interfaces;
using Azon.DAL.Repositories.Interfaces;
using Azon.Domain.Entities;
using Azon.Shared.DTOs.UserPreferenceDTOs;
using FluentValidation;

namespace Azon.BLL.Services;

public class UserPreferenceService : IUserPreferenceService
{
    private readonly IUserPreferenceRepository _preferenceRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UserPreferenceForCreateDto> _createValidator;
    private readonly IValidator<UserPreferenceForUpdateDto> _updateValidator;

    public UserPreferenceService(
        IUserPreferenceRepository preferenceRepository,
        IMapper mapper,
        IValidator<UserPreferenceForCreateDto> createValidator,
        IValidator<UserPreferenceForUpdateDto> updateValidator)
    {
        _preferenceRepository = preferenceRepository;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<UserPreferenceForResultDto?> GetByIdAsync(long id)
    {
        var preference = await _preferenceRepository.GetByIdAsync(id);

        return preference is null
            ? null
            : _mapper.Map<UserPreferenceForResultDto>(preference);
    }

    public async Task<UserPreferenceForResultDto?> GetByUserIdAsync(long userId)
    {
        var preference = await _preferenceRepository.GetByUserIdAsync(userId);

        return preference is null
            ? null
            : _mapper.Map<UserPreferenceForResultDto>(preference);
    }

    public async Task<IEnumerable<UserPreferenceForResultDto>> GetAllAsync()
    {
        var preferences = await _preferenceRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<UserPreferenceForResultDto>>(preferences);
    }

    public async Task<UserPreferenceForResultDto> CreateAsync(
        UserPreferenceForCreateDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);

        var preference = _mapper.Map<UserPreference>(dto);
        var createdPreference = await _preferenceRepository.CreateAsync(preference);

        return _mapper.Map<UserPreferenceForResultDto>(createdPreference);
    }

    public async Task<UserPreferenceForResultDto?> UpdateAsync(
        UserPreferenceForUpdateDto dto)
    {
        await _updateValidator.ValidateAndThrowAsync(dto);

        var existingPreference = await _preferenceRepository.GetByIdAsync(dto.Id);

        if (existingPreference is null)
            return null;

        _mapper.Map(dto, existingPreference);

        var updatedPreference = await _preferenceRepository.UpdateAsync(existingPreference);

        return _mapper.Map<UserPreferenceForResultDto>(updatedPreference);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var preference = await _preferenceRepository.GetByIdAsync(id);

        if (preference is null)
            return false;

        return await _preferenceRepository.DeleteAsync(id);
    }
}
