using GraphQLOrder.Api.Endpoints.AddOrder;
using GraphQLOrder.Api.Endpoints.QueryOrder;
using GraphQLOrder.Api.Entities;

namespace GraphQLOrder.Api
{
    public static class MapperExtensions
    {
        public static Order ToEntity(this AddOrderRequest request)
        {
            return new Order
            {
                Name = request.Name,
                Quantity = request.Quantity,
            };
        }

        public static AddOrderResponse ToAddContract(this Order order)
        {
            return new AddOrderResponse
            {
                Id = order.Id,
                Name = order.Name,
                Quantity = order.Quantity,
                CreationDate = order.CreationDate
            };
        }

        public static QueryOrderResponse ToQueryContract(this Order order)
        {
            return new QueryOrderResponse
            {
                Id = order.Id,
                Name = order.Name,
                Quantity = order.Quantity,
                CreationDate = order.CreationDate
            };
        }
    }
}
