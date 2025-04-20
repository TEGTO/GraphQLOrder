namespace GraphQLOrder.Api.HotChocolate.Endpoints.MutateOrder
{
    public class DeleteResponse
    {
        public required string Id { get; set; }
        public required bool Success { get; set; }
    }
}
