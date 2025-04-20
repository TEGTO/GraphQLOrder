using GraphQLOrder.Api.HotChocolate.Endpoints.MutateOrder;
using GraphQLOrder.Api.HotChocolate.Entities;

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
    }
}
