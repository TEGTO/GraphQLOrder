using GraphQLOrder.Api.Entities;

namespace GraphQLOrder.Api.Repositories
{
    public interface IOrderRepository
    {
        public Task<Order> AddOrderAsync(Order order, CancellationToken cancellationToken);
        public Task<Order?> GetOrderAsync(string id, CancellationToken cancellationToken);
    }
}