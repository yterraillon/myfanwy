using Application.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Thermo.Application;

public static class DependencyInjection
{
    public static void AddThermoApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblyContaining<Thermo>();
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
    }
}