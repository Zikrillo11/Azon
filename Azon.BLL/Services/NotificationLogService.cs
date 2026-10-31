
using AutoMapper;
using Azon.BLL.Interfaces;
using Azon.DAL.Repositories.Interfaces;
using Azon.Domain.Entities;
using Azon.Shared.DTOs.NotificationLogDTOs;
using FluentValidation;

namespace Azon.BLL.Services;

public class NotificationLogService : INotificationLogService
{
    private readonly INotificationLogRepository _notificationLogRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<NotificationLogForCreateDto> _createValidator;
    private readonly IValidator<NotificationLogForUpdateDto> _updateValidator;

    public NotificationLogService(
        INotificationLogRepository notificationLogRepository,
        IMapper mapper,
        IValidator<NotificationLogForCreateDto> createValidator,
        IValidator<NotificationLogForUpdateDto> updateValidator)
    {
        _notificationLogRepository = notificationLogRepository;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<NotificationLogForResultDto?> GetByIdAsync(long id)
    {
        var notificationLog = await _notificationLogRepository.GetByIdAsync(id);

        return notificationLog is null
            ? null
            : _mapper.Map<NotificationLogForResultDto>(notificationLog);
    }

    public async Task<IEnumerable<NotificationLogForResultDto>> GetByUserIdAsync(long userId)
    {
        var notificationLogs = await _notificationLogRepository.GetByUserIdAsync(userId);

        return _mapper.Map<IEnumerable<NotificationLogForResultDto>>(notificationLogs);
    }

    public async Task<IEnumerable<NotificationLogForShortResultDto>>
        GetPendingNotificationsAsync(DateOnly date)
    {
        var notificationLogs = await _notificationLogRepository
            .GetPendingNotificationsAsync(date);

        return _mapper.Map<IEnumerable<NotificationLogForShortResultDto>>(
            notificationLogs);
    }

    public async Task<NotificationLogForResultDto> CreateAsync(
        NotificationLogForCreateDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);

        var notificationLog = _mapper.Map<NotificationLog>(dto);
        var createdLog = await _notificationLogRepository.CreateAsync(notificationLog);

        return _mapper.Map<NotificationLogForResultDto>(createdLog);
    }

    public async Task<NotificationLogForResultDto?> UpdateAsync(
        NotificationLogForUpdateDto dto)
    {
        await _updateValidator.ValidateAndThrowAsync(dto);

        var existingLog = await _notificationLogRepository.GetByIdAsync(dto.Id);

        if (existingLog is null)
            return null;

        _mapper.Map(dto, existingLog);

        var updatedLog = await _notificationLogRepository.UpdateAsync(existingLog);

        return _mapper.Map<NotificationLogForResultDto>(updatedLog);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var notificationLog = await _notificationLogRepository.GetByIdAsync(id);

        if (notificationLog is null)
            return false;

        return await _notificationLogRepository.DeleteAsync(id);
    }
}
