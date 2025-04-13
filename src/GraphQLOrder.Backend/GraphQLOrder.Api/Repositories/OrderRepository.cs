using GraphQLOrder.Api.Data;
using GraphQLOrder.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQLOrder.Api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDatabaseRepository<OrderDbContext> databaseRepository;

        public OrderRepository(IDatabaseRepository<OrderDbContext> databaseRepository)
        {
            this.databaseRepository = databaseRepository;
        }

        public async Task<Order> AddOrderAsync(Order order, CancellationToken cancellationToken)
        {
            using var dbContext = await databaseRepository.GetDbContextAsync(cancellationToken);

            order.CreationDate = DateTime.UtcNow;

            var addedOrder = await databaseRepository.AddAsync(dbContext, order, cancellationToken);
            await databaseRepository.SaveChangesAsync(dbContext, cancellationToken);

            return addedOrder;
        }

        public async Task<Order?> GetOrderAsync(string id, CancellationToken cancellationToken)
        {
            using var dbContext = await databaseRepository.GetDbContextAsync(cancellationToken);

            var order = await databaseRepository.Query<Order>(dbContext)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

            return order;
        }
    }
}
