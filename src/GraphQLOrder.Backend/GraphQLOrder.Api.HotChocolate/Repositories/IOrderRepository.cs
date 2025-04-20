using GraphQLOrder.Api.HotChocolate.Entities;

namespace GraphQLOrder.Api.Repositories
{
    public interface IOrderRepository
    {
        public Task<Order> AddOrderAsync(Order order, CancellationToken cancellationToken);
        public Task<bool> DeleteOrderByIdAsync(string id, CancellationToken cancellationToken);
        public Task<IQueryable<Order>> QueryOrdersAsync(CancellationToken cancellationToken);
    }
}