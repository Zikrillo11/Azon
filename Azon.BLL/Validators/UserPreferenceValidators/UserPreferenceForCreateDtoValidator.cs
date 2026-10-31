using Azon.Shared.DTOs.UserPreferenceDTOs;
using FluentValidation;

namespace Azon.BLL.Validators.UserPreferenceValidators;

public class UserPreferenceForCreateDtoValidator : AbstractValidator<UserPreferenceForCreateDto>
{
    public UserPreferenceForCreateDtoValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0);

        RuleFor(x => x.CityId)
            .GreaterThan(0);
    }
}