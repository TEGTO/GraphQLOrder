namespace GraphQLOrder.Api.Endpoints.AddOrder
{
    public class AddOrderResponse
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required int Quantity { get; set; }
        public required DateTime CreationDate { get; set; }
    }
}
