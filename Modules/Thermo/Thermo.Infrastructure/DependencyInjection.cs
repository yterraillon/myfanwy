using System.Reflection;
using Application;
using Infrastructure.Databases;
using Thermo.Application.Models;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Thermo.Application;
using Thermo.Infrastructure.Jobs;

namespace Thermo.Infrastructure;

public static class DependencyInjection
{
    public static void AddThermoInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ThermoDbContext>();
        services.AddAutoMapper(configuration =>
        {
            configuration.AddMaps(Assembly.GetExecutingAssembly());
        });

        services.AddTransient<IRepository<Measurement>, Repository<Measurement, MeasurementDto>>();
        services.AddTransient<IThermoReadRepository, ThermoReadRepository>();

        services.AddQuartz(q =>
        {
            q.AddJob<CleanDataJob>(opts => opts.WithIdentity(CleanDataJob.Key));
            q.AddTrigger(opts => opts
                .ForJob(CleanDataJob.Key)
                .WithIdentity("ThermoCleanData-trigger")
                .WithCronSchedule("0 0 12 ? * SUN *")); //Run every Sunday at 12:00
        });
        
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    }
}