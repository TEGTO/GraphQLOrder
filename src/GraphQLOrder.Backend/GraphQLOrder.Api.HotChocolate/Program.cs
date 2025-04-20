using AppAny.HotChocolate.FluentValidation;
using FluentValidation;
using FluentValidation.AspNetCore;
using GraphQLOrder.Api.HotChocolate;
using GraphQLOrder.Api.HotChocolate.Data;
using GraphQLOrder.Api.HotChocolate.Endpoints.MutateOrder;
using GraphQLOrder.Api.HotChocolate.Endpoints.QueryOrder;
using GraphQLOrder.Api.HotChocolate.Repositories;
using GraphQLOrder.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<OrderDbContext>(
    builder.Configuration.GetConnectionString(ConfigurationKeys.DATABASE_CONNECTION_STRING)!,
    migrationAssembly: typeof(Program).Assembly.FullName!
);

builder.Services.AddRepository<OrderDbContext>();

builder.Services.AddSingleton<IOrderRepository, OrderRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));
ValidatorOptions.Global.LanguageManager.Enabled = false;

builder.Services.AddGraphQLServer()
    .AddQueryType<OrderQuery>()
    .AddMutationType<OrderMutation>(d =>
    {
        //d.Field(d => d.AddOrderAsync(default!, default!))
        //    .Argument("request", a => a.UseFluentValidation());
    })
    //.AddSubscriptionType<OrderSubscription>()
    //.AddInMemorySubscriptions()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .AddFluentValidation();

var app = builder.Build();

if (app.Configuration[ConfigurationKeys.EF_CREATE_DATABASE]?.ToLower() == "true")
{
    await app.ConfigureDatabaseAsync<OrderDbContext>(CancellationToken.None);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.MapGraphQL("/graphql");

await app.RunAsync();
