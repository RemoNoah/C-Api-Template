using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DotnetApiTemplate.DataAccess.Setup;
using DotnetApiTemplate.Domain.Services;
using DotnetApiTemplate.Services.Services;

namespace DotnetApiTemplate.Services.Setup;

/// <summary>
/// Contains extension methods for setting up and configuring service-related components in the service layer.
/// This static class provides extension methods to simplify the setup and initialization of services.
/// </summary>
public static class ServiceLayerSetupExtensions
{
    /// <summary>
    /// Registers all components (services, manager, etc.) from the service layer in the provided <paramref name="services"/>.
    /// </summary>
    /// <param name="services">Service collection / the current IoC.</param>
    /// <param name="configuration">Staged application configuration.</param>
    public static void AddServiceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddScoped<IAuthService, AuthService>();
        _ = services.AddScoped<IRoleService, RoleService>();
        services.AddDataBaseContext(configuration);
        services.AddUnitOfWork();
    }
}