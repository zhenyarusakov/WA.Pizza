using System;
using FluentValidation;
using WA.Pizza.Infrastructure.DTO.CatalogDTO.Catalog;

namespace WA.Pizza.Infrastructure.FluentValidation.CatalogItem;

public class CatalogItemsListItemQueryValidation: AbstractValidator<CatalogItemsListItemQuery>
{
    public CatalogItemsListItemQueryValidation()
    {
        RuleFor(x => x.Name)
            .Length(2, 50)
            .NotNull()
            .WithMessage("The Name field must be more than 2 characters but not more than 50");
        
        RuleFor(x => x.Quantity)
            .NotNull()
            .WithMessage("The Quantity field must not be empty");
        
        RuleFor(x => x.Description)
            .Length(2, 200)
            .NotNull()
            .WithMessage("The Name field must be more than 2 characters but not more than 50");
        
        RuleFor(x => x.Price)
            .InclusiveBetween(0, Decimal.MaxValue)
            .NotNull()
            .WithMessage("The Quantity field must not be empty");
    }
}