using Azon.Shared.DTOs.NotificationLogDTOs;
using FluentValidation;

namespace Azon.BLL.Validators.NotificationLogValidators;

public class NotificationLogForCreateDtoValidator : AbstractValidator<NotificationLogForCreateDto>
{
    public NotificationLogForCreateDtoValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0);

        RuleFor(x => x.PrayerDate)
            .NotEmpty();

        RuleFor(x => x.PrayerType)
            .IsInEnum();
    }
}