namespace GraphQLOrder.Api.HotChocolate.Endpoints.MutateOrder
{
    public class AddOrderRequest
    {
        public string Name { get; set; } = default!;
        public int Quantity { get; set; } = default!;
    }
}
