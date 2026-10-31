
using AutoMapper;
using Azon.BLL.Interfaces;
using Azon.DAL.Repositories.Interfaces;
using Azon.Domain.Entities;
using Azon.Shared.DTOs.CityDTOs;
using FluentValidation;

namespace Azon.BLL.Services;

public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CityForCreateDto> _createValidator;
    private readonly IValidator<CityForUpdateDto> _updateValidator;

    public CityService(
        ICityRepository cityRepository,
        IMapper mapper,
        IValidator<CityForCreateDto> createValidator,
        IValidator<CityForUpdateDto> updateValidator)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<CityForResultDto?> GetByIdAsync(long id)
    {
        var city = await _cityRepository.GetByIdAsync(id);
        return city is null ? null : _mapper.Map<CityForResultDto>(city);
    }

    public async Task<IEnumerable<CityForResultDto>> GetAllAsync()
    {
        var cities = await _cityRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CityForResultDto>>(cities);
    }

    public async Task<IEnumerable<CityForShortResultDto>> GetActiveCitiesAsync()
    {
        var cities = await _cityRepository.GetActiveCitiesAsync();
        return _mapper.Map<IEnumerable<CityForShortResultDto>>(cities);
    }

    public async Task<CityForResultDto> CreateAsync(CityForCreateDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);

        var city = _mapper.Map<City>(dto);
        var createdCity = await _cityRepository.CreateAsync(city);

        return _mapper.Map<CityForResultDto>(createdCity);
    }

    public async Task<CityForResultDto?> UpdateAsync(CityForUpdateDto dto)
    {
        await _updateValidator.ValidateAndThrowAsync(dto);

        var existingCity = await _cityRepository.GetByIdAsync(dto.Id);

        if (existingCity is null)
            return null;

        _mapper.Map(dto, existingCity);

        var updatedCity = await _cityRepository.UpdateAsync(existingCity);

        return _mapper.Map<CityForResultDto>(updatedCity);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var city = await _cityRepository.GetByIdAsync(id);

        if (city is null)
            return false;

        return await _cityRepository.DeleteAsync(id);
    }
}
