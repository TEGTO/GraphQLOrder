using AppAny.HotChocolate.FluentValidation;
using GraphQLOrder.Api.HotChocolate.Entities;
using GraphQLOrder.Api.Repositories;

namespace GraphQLOrder.Api.HotChocolate.Endpoints.MutateOrder
{
    public class OrderMutation
    {
        private readonly IOrderRepository orderRepository;

        public OrderMutation(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public Task<Order> AddOrderAsync([UseFluentValidation] AddOrderRequest request, CancellationToken cancellationToken)
        {
            return orderRepository.AddOrderAsync(request.ToEntity(), cancellationToken);
        }

        public async Task<DeleteResponse> DeleteOrderByIdAsync(string id, CancellationToken cancellationToken)
        {
            var result = await orderRepository.DeleteOrderByIdAsync(id, cancellationToken);
            return new DeleteResponse
            {
                Id = id,
                Success = result,
            };
        }
    }
}
