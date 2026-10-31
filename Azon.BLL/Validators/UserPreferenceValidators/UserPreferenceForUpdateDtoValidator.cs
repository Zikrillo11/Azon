using Azon.Shared.DTOs.UserPreferenceDTOs;
using FluentValidation;

namespace Azon.BLL.Validators.UserPreferenceValidators;

public class UserPreferenceForUpdateDtoValidator : AbstractValidator<UserPreferenceForUpdateDto>
{
    public UserPreferenceForUpdateDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.CityId)
            .GreaterThan(0);
    }
}