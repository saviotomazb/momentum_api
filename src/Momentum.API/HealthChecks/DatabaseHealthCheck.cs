using Microsoft.Extensions.Diagnostics.HealthChecks;
using Momentum.Infrastructure.Persistence.Context;

namespace Momentum.API.HealthChecks;

public class DatabaseHealthCheck : IHealthCheck
{
    private readonly AppDbContext _context;

    public DatabaseHealthCheck(AppDbContext context)
    {
        _context = context;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var canConnect = await _context.Database.CanConnectAsync(cancellationToken);

        return canConnect
            ? HealthCheckResult.Healthy("Database connection is healthy.")
            : HealthCheckResult.Unhealthy("Database connection is unavailable.");
    }
}
