using Azon.Shared.DTOs.UserDTOs;
using FluentValidation;

namespace Azon.BLL.Validators.UserValidators;

public class UserForCreateDtoValidator : AbstractValidator<UserForCreateDto>
{
    public UserForCreateDtoValidator()
    {
        RuleFor(x => x.TelegramUserId)
            .GreaterThan(0)
            .WithMessage("Telegram User ID musbat bo'lishi kerak.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("Ism bo'sh bo'lmasligi kerak.")
            .MaximumLength(100);

        RuleFor(x => x.TelegramUsername)
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .MaximumLength(100);

        RuleFor(x => x.LanguageCode)
            .NotEmpty()
            .Must(x => new[] { "uz", "en", "ru" }.Contains(x))
            .WithMessage("Til kodi uz, en yoki ru bo'lishi kerak.");
    }
}