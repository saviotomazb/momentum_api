using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Momentum.Application.Common.Security;
using Momentum.Application.Interfaces.Persistence;
using Momentum.Application.Interfaces.Habits;
using Momentum.Application.Services.Habits;
using Momentum.Infrastructure.Persistence.Context;
using Momentum.Infrastructure.Persistence.Repositories;

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

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IHabitRepository, HabitRepository>();
        
        services.AddScoped<IHabitService, HabitService>();

        services.AddScoped<JwtTokenGenerator>();

        return services;
    }
}