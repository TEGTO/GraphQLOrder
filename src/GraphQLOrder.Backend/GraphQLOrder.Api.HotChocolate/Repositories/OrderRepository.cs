using GraphQLOrder.Api.HotChocolate.Data;
using GraphQLOrder.Api.HotChocolate.Entities;
using GraphQLOrder.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GraphQLOrder.Api.HotChocolate.Repositories
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

        public async Task<bool> DeleteOrderByIdAsync(string id, CancellationToken cancellationToken)
        {
            using var dbContext = await databaseRepository.GetDbContextAsync(cancellationToken);

            var order = await databaseRepository.Query<Order>(dbContext).FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

            if (order != null)
            {
                dbContext.Remove(order);
                await databaseRepository.SaveChangesAsync(dbContext, cancellationToken);
                return true;
            }

            return false;
        }

        public async Task<IQueryable<Order>> QueryOrdersAsync(CancellationToken cancellationToken)
        {
            var dbContext = await databaseRepository.GetDbContextAsync(cancellationToken);
            return databaseRepository.Query<Order>(dbContext);
        }
    }
}
