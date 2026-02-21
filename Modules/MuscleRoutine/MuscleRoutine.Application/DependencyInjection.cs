using Microsoft.Extensions.DependencyInjection;

namespace MuscleRoutine.Application;

public static class DependencyInjection
{
    public static void AddMuscleRoutineApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<MuscleRoutine>());
    }
}