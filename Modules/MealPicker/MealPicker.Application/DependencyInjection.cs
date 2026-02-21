using Microsoft.Extensions.DependencyInjection;

namespace MealPicker.Application;

public static class DependencyInjection
{
    public static void AddMealPickerApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<MealPicker>());
    }
}