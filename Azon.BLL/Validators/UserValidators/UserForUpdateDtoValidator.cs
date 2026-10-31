using Azon.Shared.DTOs.UserDTOs;
using FluentValidation;

namespace Azon.BLL.Validators.UserValidators;

public class UserForUpdateDtoValidator : AbstractValidator<UserForUpdateDto>
{
    public UserForUpdateDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .When(x => x.FirstName is not null)
            .MaximumLength(100);

        RuleFor(x => x.TelegramUsername)
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .MaximumLength(100);

        RuleFor(x => x.LanguageCode)
            .Must(x => x is null || new[] { "uz", "en", "ru" }.Contains(x))
            .WithMessage("Til kodi uz, en yoki ru bo'lishi kerak.");
    }
}