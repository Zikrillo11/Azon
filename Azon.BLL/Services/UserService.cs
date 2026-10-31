
using AutoMapper;
using Azon.BLL.Interfaces;
using Azon.DAL.Repositories.Interfaces;
using Azon.Domain.Entities;
using Azon.Shared.DTOs.UserDTOs;
using FluentValidation;

namespace Azon.BLL.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UserForCreateDto> _createValidator;
    private readonly IValidator<UserForUpdateDto> _updateValidator;

    public UserService(
        IUserRepository userRepository,
        IMapper mapper,
        IValidator<UserForCreateDto> createValidator,
        IValidator<UserForUpdateDto> updateValidator)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<UserForResultDto?> GetByIdAsync(long id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        return user is null
            ? null
            : _mapper.Map<UserForResultDto>(user);
    }

    public async Task<UserForResultDto?> GetByTelegramUserIdAsync(long telegramUserId)
    {
        var user = await _userRepository.GetByTelegramUserIdAsync(telegramUserId);

        return user is null
            ? null
            : _mapper.Map<UserForResultDto>(user);
    }

    public async Task<IEnumerable<UserForResultDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<UserForResultDto>>(users);
    }

    public async Task<UserForResultDto> CreateAsync(UserForCreateDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);

        var existingUser = await _userRepository
            .GetByTelegramUserIdAsync(dto.TelegramUserId);

        if (existingUser is not null)
            throw new InvalidOperationException(
                "Bu Telegram foydalanuvchisi allaqachon mavjud.");

        var user = _mapper.Map<User>(dto);

        var createdUser = await _userRepository.CreateAsync(user);

        return _mapper.Map<UserForResultDto>(createdUser);
    }

    public async Task<UserForResultDto?> UpdateAsync(UserForUpdateDto dto)
    {
        await _updateValidator.ValidateAndThrowAsync(dto);

        var existingUser = await _userRepository.GetByIdAsync(dto.Id);

        if (existingUser is null)
            return null;

        _mapper.Map(dto, existingUser);

        var updatedUser = await _userRepository.UpdateAsync(existingUser);

        return _mapper.Map<UserForResultDto>(updatedUser);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user is null)
            return false;

        return await _userRepository.DeleteAsync(id);
    }
}
