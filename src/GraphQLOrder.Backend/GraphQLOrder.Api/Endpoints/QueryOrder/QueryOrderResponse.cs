namespace GraphQLOrder.Api.Endpoints.QueryOrder
{
    public class QueryOrderResponse
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required int Quantity { get; set; }
        public required DateTime CreationDate { get; set; }
    }
}
