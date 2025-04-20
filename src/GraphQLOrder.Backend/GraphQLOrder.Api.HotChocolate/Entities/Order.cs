namespace GraphQLOrder.Api.HotChocolate.Entities
{
    public class Order
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public DateTime CreationDate { get; set; }
    }
}
