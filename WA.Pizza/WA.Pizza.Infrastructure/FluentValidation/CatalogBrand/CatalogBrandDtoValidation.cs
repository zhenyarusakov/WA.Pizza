using FluentValidation;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.CatalogBrand;

namespace WA.Pizza.Infrastructure.FluentValidation.CatalogBrand;

public class CatalogBrandDtoValidation: AbstractValidator<CatalogBrandDto>
{
    public CatalogBrandDtoValidation()
    {
        RuleFor(x => x.Name)
            .Length(2, 50)
            .NotNull()
            .WithMessage("The Name field must be more than 2 characters but not more than 50");
        
        RuleFor(x => x.Description)
            .Length(2, 200)
            .NotNull()
            .WithMessage("The Name field must be more than 2 characters but not more than 50");
    }
}