using Azon.Shared.DTOs.PrayerTimeDTOs;
using FluentValidation;

namespace Azon.BLL.Validators.PrayerTimeValidators;

public class PrayerTimeForUpdateDtoValidator : AbstractValidator<PrayerTimeForUpdateDto>
{
    public PrayerTimeForUpdateDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.CityId)
            .GreaterThan(0);

        RuleFor(x => x.PrayerDate)
            .NotEmpty();

        RuleFor(x => x.Fajr)
            .LessThan(x => x.Sunrise);

        RuleFor(x => x.Sunrise)
            .LessThan(x => x.Dhuhr);

        RuleFor(x => x.Dhuhr)
            .LessThan(x => x.Asr);

        RuleFor(x => x.Asr)
            .LessThan(x => x.Maghrib);

        RuleFor(x => x.Maghrib)
            .LessThan(x => x.Isha);

        RuleFor(x => x.CalculationMethod)
            .MaximumLength(100);

        RuleFor(x => x.Source)
            .MaximumLength(100);
    }
}