using GraphQLOrder.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace GraphQLOrder.Api
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepository<Context>(this IServiceCollection services) where Context : DbContext
        {
            services.AddSingleton<IDatabaseRepository<Context>, DatabaseRepository<Context>>();
            return services;
        }

        public static IServiceCollection AddDbContextFactory<Context>(
            this IServiceCollection services,
            string connectionString,
            string? migrationAssembly = null,
            Action<NpgsqlDbContextOptionsBuilder>? dbAdditionalConfig = null,
            Action<DbContextOptionsBuilder>? additionalConfig = null
        ) where Context : DbContext
        {
            services.AddDbContextFactory<Context>(options =>
            {
                options.UseNpgsql(connectionString, b =>
                {
                    if (!string.IsNullOrEmpty(migrationAssembly))
                    {
                        b.MigrationsAssembly(migrationAssembly);
                    }
                    dbAdditionalConfig?.Invoke(b);
                });

                options.UseSnakeCaseNamingConvention();

                additionalConfig?.Invoke(options);
            });

            return services;
        }

        public static async Task<IApplicationBuilder> ConfigureDatabaseAsync<TContext>(this IApplicationBuilder builder, CancellationToken cancellationToken) where TContext : DbContext
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<IApplicationBuilder>>();
                var repository = services.GetRequiredService<IDatabaseRepository<TContext>>();
                try
                {
                    logger.LogInformation("Applying database migrations...");
                    await repository.MigrateDatabaseAsync(cancellationToken);
                    logger.LogInformation("Database migrations applied successfully.");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }
            return builder;
        }
    }
}
