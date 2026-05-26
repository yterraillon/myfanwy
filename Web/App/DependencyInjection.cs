using CineRoulette.Function;
using ComicGrabber.Application;
using ComicGrabber.Infrastructure;
using EnBref.Application;
using EnBref.Infrastructure;
using MealPicker.Application;
using MealPicker.Infrastructure;
using MuscleRoutine.Application;
using MuscleRoutine.Infrastructure;
using Thermo.Application;
using Settings = Infrastructure.Notifications.Settings;

namespace Web.App;

public static class DependencyInjection
{
    public static void LoadModules(this IServiceCollection services, bool isDevelopment)
    {
        services.AddThermoApplication();
        services.AddThermoInfrastructure();
        services.AddEnBrefApplication();
        services.AddEnBrefInfrastructure();
        services.AddComicGrabberApplication();
        services.AddComicGrabberInfrastructure(isUsingDocker: isDevelopment);
        services.AddMealPickerApplication();
        services.AddMealPickerInfrastructure();
        services.AddMuscleRoutineApplication();
        services.AddMuscleRoutineInfrastructure();
        services.AddCineRouletteFunction();
    }

    public static void LoadConfigurations(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddSingleton<EnBref.Infrastructure.Settings>(_ => new EnBref.Infrastructure.Settings
        {
            ClaudeApiKey = builder.Environment.IsDevelopment() ?
                builder.Configuration["ClaudeApiKey"] :
                Environment.GetEnvironmentVariable("ClaudeApiKey"),
            GithubToken = builder.Environment.IsDevelopment() ?
                builder.Configuration["GithubToken"] :
                Environment.GetEnvironmentVariable("GithubToken"),
        });

        services.AddSingleton<MealPicker.Infrastructure.Settings>(_ => new MealPicker.Infrastructure.Settings
        {
            MealPickerStorageAccountConnectionString = builder.Environment.IsDevelopment() ? 
                builder.Configuration["MealPickerStorageAccountConnectionString"] : 
                Environment.GetEnvironmentVariable("MealPickerStorageAccountConnectionString"),  
        });
        
        services.AddSingleton<MuscleRoutine.Infrastructure.Settings>(_ => new MuscleRoutine.Infrastructure.Settings
        {
            MuscleRoutineStorageAccountConnectionString = builder.Environment.IsDevelopment() ? 
                builder.Configuration["MuscleRoutineStorageAccountConnectionString"] : 
                Environment.GetEnvironmentVariable("MuscleRoutineStorageAccountConnectionString"),  
        });

        services.AddSingleton<Settings>(_ => new Settings
        {
            NtfyToken = builder.Environment.IsDevelopment() ?
                builder.Configuration["NtfyToken"] :
                Environment.GetEnvironmentVariable("NtfyToken")
        });

        var autoMapperLicenseKey = builder.Environment.IsDevelopment()
            ? builder.Configuration["AutoMapperLicenseKey"]
            : Environment.GetEnvironmentVariable("AutoMapperLicenseKey");

        services.AddAutoMapper(cfg =>
        {
            cfg.LicenseKey = autoMapperLicenseKey;
            cfg.AddMaps(typeof(EnBref.Infrastructure.DependencyInjection).Assembly);
            cfg.AddMaps(typeof(Thermo.Infrastructure.DependencyInjection).Assembly);
        });
    }
}