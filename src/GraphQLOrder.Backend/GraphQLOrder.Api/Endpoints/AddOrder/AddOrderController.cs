using FluentValidation;
using GraphQL.AspNet.Attributes;
using GraphQL.AspNet.Controllers;
using GraphQL.AspNet.Interfaces.Controllers;
using GraphQLOrder.Api.Repositories;

namespace GraphQLOrder.Api.Endpoints.AddOrder
{
    [GraphRoute("order")]
    public class AddOrderController : GraphController
    {
        private readonly IOrderRepository orderRepository;
        private readonly IValidator<AddOrderRequest> validator;

        public AddOrderController(IOrderRepository orderRepository, IValidator<AddOrderRequest> validator)
        {
            this.orderRepository = orderRepository;
            this.validator = validator;
        }

        [Mutation(typeof(AddOrderResponse))]
        public async Task<IGraphActionResult> AddOrderAsync(AddOrderRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BadRequest(string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            var order = request.ToEntity();

            var response = await orderRepository.AddOrderAsync(order, cancellationToken);

            return Ok(response.ToAddContract());
        }
    }
}