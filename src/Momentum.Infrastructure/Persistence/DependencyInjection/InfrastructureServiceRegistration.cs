using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Momentum.Infrastructure.Persistence.Context;

namespace Momentum.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var dbHost = configuration["DB_HOST"];
        var dbPort = configuration["DB_PORT"];
        var dbName = configuration["POSTGRES_DB"];
        var dbUser = configuration["POSTGRES_USER"];
        var dbPassword = configuration["POSTGRES_PASSWORD"];

        var connectionString =
            $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPassword}";

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }
}