
using AutoMapper;
using Azon.BLL.Interfaces;
using Azon.DAL.Repositories.Interfaces;
using Azon.Domain.Entities;
using Azon.Shared.DTOs.PrayerTimeDTOs;
using FluentValidation;

namespace Azon.BLL.Services;

public class PrayerTimeService : IPrayerTimeService
{
    private readonly IPrayerTimeRepository _prayerTimeRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<PrayerTimeForCreateDto> _createValidator;
    private readonly IValidator<PrayerTimeForUpdateDto> _updateValidator;

    public PrayerTimeService(
        IPrayerTimeRepository prayerTimeRepository,
        IMapper mapper,
        IValidator<PrayerTimeForCreateDto> createValidator,
        IValidator<PrayerTimeForUpdateDto> updateValidator)
    {
        _prayerTimeRepository = prayerTimeRepository;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<PrayerTimeForResultDto?> GetByIdAsync(long id)
    {
        var prayerTime = await _prayerTimeRepository.GetByIdAsync(id);
        return prayerTime is null
            ? null
            : _mapper.Map<PrayerTimeForResultDto>(prayerTime);
    }

    public async Task<PrayerTimeForResultDto?> GetByCityAndDateAsync(
        long cityId, DateOnly date)
    {
        var prayerTime = await _prayerTimeRepository
            .GetByCityAndDateAsync(cityId, date);

        return prayerTime is null
            ? null
            : _mapper.Map<PrayerTimeForResultDto>(prayerTime);
    }

    public async Task<IEnumerable<PrayerTimeForResultDto>> GetByCityIdAsync(long cityId)
    {
        var prayerTimes = await _prayerTimeRepository.GetByCityIdAsync(cityId);
        return _mapper.Map<IEnumerable<PrayerTimeForResultDto>>(prayerTimes);
    }

    public async Task<PrayerTimeForResultDto> CreateAsync(PrayerTimeForCreateDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);

        var prayerTime = _mapper.Map<PrayerTime>(dto);
        var createdPrayerTime = await _prayerTimeRepository.CreateAsync(prayerTime);

        return _mapper.Map<PrayerTimeForResultDto>(createdPrayerTime);
    }

    public async Task<PrayerTimeForResultDto?> UpdateAsync(PrayerTimeForUpdateDto dto)
    {
        await _updateValidator.ValidateAndThrowAsync(dto);

        var existingPrayerTime = await _prayerTimeRepository.GetByIdAsync(dto.Id);

        if (existingPrayerTime is null)
            return null;

        _mapper.Map(dto, existingPrayerTime);

        var updatedPrayerTime = await _prayerTimeRepository.UpdateAsync(existingPrayerTime);

        return _mapper.Map<PrayerTimeForResultDto>(updatedPrayerTime);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var prayerTime = await _prayerTimeRepository.GetByIdAsync(id);

        if (prayerTime is null)
            return false;

        return await _prayerTimeRepository.DeleteAsync(id);
    }
}
