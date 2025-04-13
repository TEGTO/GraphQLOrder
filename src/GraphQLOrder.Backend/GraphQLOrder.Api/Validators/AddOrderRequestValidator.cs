using FluentValidation;
using GraphQLOrder.Api.Endpoints.AddOrder;

namespace GraphQLOrder.Api.Validators
{
    public class AddOrderRequestValidator : AbstractValidator<AddOrderRequest>
    {
        public AddOrderRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(x => x.Quantity)
                .GreaterThan(0);
        }
    }
}
