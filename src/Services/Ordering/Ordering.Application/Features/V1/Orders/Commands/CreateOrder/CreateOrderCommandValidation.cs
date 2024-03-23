using FluentValidation;

namespace Ordering.Application.Features.V1.Orders;

public class CreateOrderCommandValidation : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidation()
    {
        RuleFor(p => p.UserName).NotEmpty().WithMessage("{UserName} is required.");
    }
}
