
using AutoMapper;
using Azon.BLL.Interfaces;
using Azon.DAL.Repositories.Interfaces;
using Azon.Domain.Entities;
using Azon.Shared.DTOs.NotificationLogDTOs;

namespace Azon.BLL.Services;

public sealed class NotificationLogService : INotificationLogService
{
    private readonly INotificationLogRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public NotificationLogService(
        INotificationLogRepository repository,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _repository = repository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<NotificationLogForResultDto?> GetByIdAsync(long id)
    {
        var log = await _repository.GetByIdAsync(id);

        return log is null
            ? null
            : _mapper.Map<NotificationLogForResultDto>(log);
    }

    public async Task<IEnumerable<NotificationLogForResultDto>> GetByUserIdAsync(
        long userId)
    {
        var logs = await _repository.GetByUserIdAsync(userId);

        return _mapper.Map<IEnumerable<NotificationLogForResultDto>>(logs);
    }

    public async Task<IEnumerable<NotificationLogForShortResultDto>>
        GetPendingNotificationsAsync(DateOnly date)
    {
        var logs = await _repository.GetPendingNotificationsAsync(date);

        return _mapper.Map<IEnumerable<NotificationLogForShortResultDto>>(logs);
    }

    public async Task<NotificationLogForResultDto> CreateAsync(
        NotificationLogForCreateDto dto)
    {
        var user = await _userRepository.GetByIdAsync(dto.UserId);

        if (user is null)
            throw new KeyNotFoundException("User not found.");

        var log = _mapper.Map<NotificationLog>(dto);

        await _repository.CreateAsync(log);

        return _mapper.Map<NotificationLogForResultDto>(log);
    }

    public async Task<NotificationLogForResultDto?> UpdateAsync(
        NotificationLogForUpdateDto dto)
    {
        var log = await _repository.GetByIdAsync(dto.Id);

        if (log is null)
            return null;

        _mapper.Map(dto, log);

        await _repository.UpdateAsync(log);

        return _mapper.Map<NotificationLogForResultDto>(log);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var log = await _repository.GetByIdAsync(id);

        if (log is null)
            return false;

        await _repository.DeleteAsync(id);

        return true;
    }
}
