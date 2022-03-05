using FluentValidation;
using WA.Pizza.Infrastructure.DTO.AdvertisementDTO;

namespace WA.Pizza.Infrastructure.FluentValidation.Advertisement;

public class AdvertisementDtoValidation: AbstractValidator<AdvertisementDto>
{
    public AdvertisementDtoValidation()
    {
        RuleFor(x => x.Name)
            .Length(2, 128)
            .NotNull()
            .WithMessage("The Name field must be more than 2 characters but not more than 50");

        RuleFor(x => x.WebSite)
            .Length(2, 128)
            .NotNull()
            .WithMessage("The WebSite field must be more than 2 characters but not more than 50");
        
        RuleFor(x => x.Img)
            .Length(2, 256)
            .NotNull()
            .WithMessage("The Img field must be more than 2 characters but not more than 50");
    }
}