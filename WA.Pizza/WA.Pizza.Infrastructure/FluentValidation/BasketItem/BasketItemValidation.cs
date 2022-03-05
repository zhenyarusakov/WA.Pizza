using FluentValidation;
using WA.Pizza.Infrastructure.DTO.BasketDTO.BasketItem;

namespace WA.Pizza.Infrastructure.FluentValidation.BasketItem;

public class BasketItemValidation: AbstractValidator<BasketItemDto>
{
    public BasketItemValidation()
    {
        RuleFor(x => x.Name)
            .Length(2, 50)
            .NotNull()
            .WithMessage("The Name field must be more than 2 characters but not more than 50");
        
        RuleFor(x => x.Description)
            .Length(2, 200)
            .NotNull()
            .WithMessage("The Name field must be more than 2 characters but not more than 50");
        
        RuleFor(x => x.Price)
            .InclusiveBetween(0, 10)
            .NotNull();
    }
}