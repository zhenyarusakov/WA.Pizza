using FluentValidation;
using WA.Pizza.Infrastructure.DTO.OrderDTO.OrderItem;

namespace WA.Pizza.Infrastructure.FluentValidation.OrderItem;

public class CreateOrderItemRequestValidation: AbstractValidator<CreateOrderItemRequest>
{
    public CreateOrderItemRequestValidation()
    {
        RuleFor(x => x.Name)
            .Length(2, 50)
            .NotNull()
            .WithMessage("The Name field must be more than 2 characters but not more than 50");

        RuleFor(x => x.Description)
            .Length(2, 150)
            .NotNull()
            .WithMessage("The Description field must be more than 2 characters but not more than 50");

        RuleFor(x => x.Quantity)
            .NotNull()
            .WithMessage("The Quantity field must not be empty");
    }
}