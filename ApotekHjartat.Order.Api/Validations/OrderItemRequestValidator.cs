using ApotekHjartat.Order.Api.DataTransferObjects.RequestDTOs;
using FluentValidation;

namespace ApotekHjartat.Order.Api.Validations
{
    public class OrderItemRequestValidator : AbstractValidator<OrderItemRequestDTO>
    {
        public OrderItemRequestValidator()
        {
            RuleFor(x => x.ProductId)
                    .NotEmpty()
                    .WithMessage("ProductId is required");
            RuleFor(x => x.Quantity)
                .NotNull()
                .NotEmpty()
                .WithMessage("Quantity is required");
        }
    }
}
