using GraphQLOrder.Api.HotChocolate.Entities;
using GraphQLOrder.Api.Repositories;

namespace GraphQLOrder.Api.HotChocolate.Endpoints.QueryOrder
{
    public class OrderQuery
    {
        private readonly IOrderRepository repository;

        public OrderQuery(IOrderRepository repository)
        {
            this.repository = repository;
        }

        [UseSingleOrDefault]
        [UseProjection]
        public async Task<IQueryable<Order>> GetOrderById(string id, CancellationToken cancellationToken)
        {
            return (await repository.QueryOrdersAsync(cancellationToken)).Where(o => o.Id == id);
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public Task<IQueryable<Order>> GetOrders(CancellationToken cancellationToken)
        {
            return repository.QueryOrdersAsync(cancellationToken);
        }
    }
}
