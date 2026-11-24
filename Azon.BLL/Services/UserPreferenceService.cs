
using AutoMapper;
using Azon.BLL.Interfaces;
using Azon.DAL.Repositories.Interfaces;
using Azon.Domain.Entities;
using Azon.Shared.DTOs.UserPreferenceDTOs;

namespace Azon.BLL.Services;

public sealed class UserPreferenceService : IUserPreferenceService
{
    private readonly IUserPreferenceRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly ICityRepository _cityRepository;
    private readonly IMapper _mapper;

    public UserPreferenceService(
        IUserPreferenceRepository repository,
        IUserRepository userRepository,
        ICityRepository cityRepository,
        IMapper mapper)
    {
        _repository = repository;
        _userRepository = userRepository;
        _cityRepository = cityRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserPreferenceForResultDto>> GetAllAsync()
    {
        var preferences = await _repository.GetAllAsync();

        return _mapper.Map<IEnumerable<UserPreferenceForResultDto>>(
            preferences);
    }

    public async Task<UserPreferenceForResultDto?> GetByIdAsync(long id)
    {
        var preference = await _repository.GetByIdAsync(id);

        return preference is null
            ? null
            : _mapper.Map<UserPreferenceForResultDto>(preference);
    }

    public async Task<UserPreferenceForResultDto?> GetByUserIdAsync(long userId)
    {
        var preference = await _repository.GetByUserIdAsync(userId);

        return preference is null
            ? null
            : _mapper.Map<UserPreferenceForResultDto>(preference);
    }

    public async Task<UserPreferenceForResultDto> CreateAsync(
        UserPreferenceForCreateDto dto)
    {
        var user = await _userRepository.GetByIdAsync(dto.UserId);

        if (user is null)
            throw new KeyNotFoundException("User not found.");

        var city = await _cityRepository.GetByIdAsync(dto.CityId);

        if (city is null)
            throw new KeyNotFoundException("City not found.");

        var existing = await _repository.GetByUserIdAsync(dto.UserId);

        if (existing is not null)
            throw new InvalidOperationException(
                "User preference already exists.");

        var preference = _mapper.Map<UserPreference>(dto);

        await _repository.CreateAsync(preference);

        return _mapper.Map<UserPreferenceForResultDto>(preference);
    }

    public async Task<UserPreferenceForResultDto?> UpdateAsync(
        UserPreferenceForUpdateDto dto)
    {
        var preference = await _repository.GetByIdAsync(dto.Id);

        if (preference is null)
            return null;

        var city = await _cityRepository.GetByIdAsync(dto.CityId);

        if (city is null)
            throw new KeyNotFoundException("City not found.");

        _mapper.Map(dto, preference);

        await _repository.UpdateAsync(preference);

        return _mapper.Map<UserPreferenceForResultDto>(preference);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var preference = await _repository.GetByIdAsync(id);

        if (preference is null)
            return false;

        await _repository.DeleteAsync(id);

        return true;
    }
}
