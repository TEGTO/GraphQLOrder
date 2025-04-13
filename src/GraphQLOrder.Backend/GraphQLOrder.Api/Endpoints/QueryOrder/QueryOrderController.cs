using GraphQL.AspNet.Attributes;
using GraphQL.AspNet.Controllers;
using GraphQL.AspNet.Interfaces.Controllers;
using GraphQLOrder.Api.Repositories;

namespace GraphQLOrder.Api.Endpoints.QueryOrder
{
    [GraphRoute("order")]
    public class QueryOrderController : GraphController
    {
        private readonly IOrderRepository orderRepository;

        public QueryOrderController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        [Query(typeof(QueryOrderResponse))]
        public async Task<IGraphActionResult> GetOrderByIdAsync(string id, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetOrderAsync(id, cancellationToken);

            if (order == null)
            {
                return NotFound($"Order not found with id: {id}");
            }

            return Ok(order.ToQueryContract());
        }
    }
}
