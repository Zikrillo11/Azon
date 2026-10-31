using Azon.Shared.DTOs.MosqueDTOs;
using FluentValidation;

namespace Azon.BLL.Validators.MosqueValidators;

public class MosqueForUpdateDtoValidator : AbstractValidator<MosqueForUpdateDto>
{
    public MosqueForUpdateDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Address)
            .MaximumLength(500);

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(30);

        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90);

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180);

        RuleFor(x => x.OpenStreetMapId)
            .MaximumLength(100);

        RuleFor(x => x.CityId)
            .GreaterThan(0)
            .When(x => x.CityId.HasValue);
    }
}