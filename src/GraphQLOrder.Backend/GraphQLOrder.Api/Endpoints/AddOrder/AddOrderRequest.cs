namespace GraphQLOrder.Api.Endpoints.AddOrder
{
    public class AddOrderRequest
    {
        public string Name { get; set; } = default!;
        public int Quantity { get; set; } = default!;
    }
}
