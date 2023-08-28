using ApotekHjartat.Order.Api.DataTransferObjects.RequestDTOs;
using FluentValidation;

namespace ApotekHjartat.Order.Api.Validations
{
    public class OrderRequestValidator : AbstractValidator<OrderRequestDTO>
    {
        public OrderRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .NotEmpty()
                .WithMessage("UserId is required");
            RuleFor(x => x.OrderItems)
                .NotEmpty()
                .WithMessage("OrderItems is required");
        }
    }
}
