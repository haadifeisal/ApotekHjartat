using ApotekHjartat.Order.Api.DataTransferObjects.RequestDTOs;
using FluentValidation;

namespace ApotekHjartat.Order.Api.Validations
{
    public class UpdateOrderStatusRequestValidator : AbstractValidator<UpdateOrderStatusRequestDTO>
    {
        public UpdateOrderStatusRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .NotEmpty()
                .WithMessage("UserId is required");
            RuleFor(x => x.Status)
                .NotNull()
                .NotEmpty()
                .WithMessage("Status is required")
                .InclusiveBetween(0, 2)
                .WithMessage("Status must be between 0 and 2");
        }
    }
}
