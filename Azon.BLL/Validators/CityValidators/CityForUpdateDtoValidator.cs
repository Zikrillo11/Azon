using Azon.Shared.DTOs.CityDTOs;
using FluentValidation;

namespace Azon.BLL.Validators.CityValidators;

public class CityForUpdateDtoValidator : AbstractValidator<CityForUpdateDto>
{
    public CityForUpdateDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.CountryCode)
            .NotEmpty()
            .Length(2);

        RuleFor(x => x.TimeZoneId)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90);

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180);
    }
}