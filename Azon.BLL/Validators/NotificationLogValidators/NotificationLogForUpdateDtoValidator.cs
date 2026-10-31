using Azon.Shared.DTOs.NotificationLogDTOs;
using FluentValidation;

namespace Azon.BLL.Validators.NotificationLogValidators;

public class NotificationLogForUpdateDtoValidator : AbstractValidator<NotificationLogForUpdateDto>
{
    public NotificationLogForUpdateDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.NotificationStatus)
            .IsInEnum();

        RuleFor(x => x.ErrorMessage)
            .MaximumLength(2000);
    }
}