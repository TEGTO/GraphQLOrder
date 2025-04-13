using FluentValidation;
using FluentValidation.AspNetCore;
using GraphQL.AspNet.Configuration;
using GraphQLOrder.Api;
using GraphQLOrder.Api.Data;
using GraphQLOrder.Api.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<OrderDbContext>(
    builder.Configuration.GetConnectionString(ConfigurationKeys.DATABASE_CONNECTION_STRING)!,
    migrationAssembly: "GraphQLOrder.Api"
);

builder.Services.AddRepository<OrderDbContext>();

builder.Services.AddSingleton<IOrderRepository, OrderRepository>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));
ValidatorOptions.Global.LanguageManager.Enabled = false;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGraphQL(options =>
{
    options.AddAssembly(Assembly.GetAssembly(typeof(Program))); // For interfaces
    //options.AddGraphType<AddOrderResponse>(); // OR use [GraphType] attribute
});

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

app.UseGraphQL();
app.UseGraphQLGraphiQL("/graphiql"); // UI

await app.RunAsync();
