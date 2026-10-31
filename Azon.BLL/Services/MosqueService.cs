
using AutoMapper;
using Azon.BLL.Interfaces;
using Azon.DAL.Repositories.Interfaces;
using Azon.Domain.Entities;
using Azon.Shared.DTOs.MosqueDTOs;
using FluentValidation;

namespace Azon.BLL.Services;

public class MosqueService : IMosqueService
{
    private readonly IMosqueRepository _mosqueRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<MosqueForCreateDto> _createValidator;
    private readonly IValidator<MosqueForUpdateDto> _updateValidator;

    public MosqueService(
        IMosqueRepository mosqueRepository,
        IMapper mapper,
        IValidator<MosqueForCreateDto> createValidator,
        IValidator<MosqueForUpdateDto> updateValidator)
    {
        _mosqueRepository = mosqueRepository;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<MosqueForResultDto?> GetByIdAsync(long id)
    {
        var mosque = await _mosqueRepository.GetByIdAsync(id);
        return mosque is null ? null : _mapper.Map<MosqueForResultDto>(mosque);
    }

    public async Task<IEnumerable<MosqueForResultDto>> GetAllAsync()
    {
        var mosques = await _mosqueRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<MosqueForResultDto>>(mosques);
    }

    public async Task<IEnumerable<MosqueForShortResultDto>> GetByCityIdAsync(long cityId)
    {
        var mosques = await _mosqueRepository.GetByCityIdAsync(cityId);
        return _mapper.Map<IEnumerable<MosqueForShortResultDto>>(mosques);
    }

    public async Task<MosqueForResultDto> CreateAsync(MosqueForCreateDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);

        var mosque = _mapper.Map<Mosque>(dto);
        var createdMosque = await _mosqueRepository.CreateAsync(mosque);

        return _mapper.Map<MosqueForResultDto>(createdMosque);
    }

    public async Task<MosqueForResultDto?> UpdateAsync(MosqueForUpdateDto dto)
    {
        await _updateValidator.ValidateAndThrowAsync(dto);

        var existingMosque = await _mosqueRepository.GetByIdAsync(dto.Id);

        if (existingMosque is null)
            return null;

        _mapper.Map(dto, existingMosque);

        var updatedMosque = await _mosqueRepository.UpdateAsync(existingMosque);

        return _mapper.Map<MosqueForResultDto>(updatedMosque);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var mosque = await _mosqueRepository.GetByIdAsync(id);

        if (mosque is null)
            return false;

        return await _mosqueRepository.DeleteAsync(id);
    }
}
