using FluentValidation;
using WA.Pizza.Infrastructure.DTO.AdsClientDTO;

namespace WA.Pizza.Infrastructure.FluentValidation.AdsClient;

public class AdsClientDtoValidation: AbstractValidator<AdsClientDto>
{
    public AdsClientDtoValidation()
    {
        RuleFor(x => x.Name)
            .Length(2, 128)
            .NotNull()
            .WithMessage("The Name field must be more than 2 characters but not more than 50");

        RuleFor(x => x.WebSite)
            .Length(2, 128)
            .NotNull()
            .WithMessage("The WebSite field must be more than 2 characters but not more than 50");
    }
}