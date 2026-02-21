using System.Net.Http.Headers;
using Application.ObjectStorage;
using MealPicker.Application.Models;
using MealPicker.Infrastructure.GithubCdn;
using Microsoft.Extensions.DependencyInjection;

namespace MealPicker.Infrastructure;

public static class DependencyInjection
{
    public static void AddMealPickerInfrastructure(this IServiceCollection services)
    {
        services.AddHttpClient<IObjectStorageReader<List<Meal>>, GithubCdnClient>("meal-picker-client", client =>
        {
            client.BaseAddress = new Uri("https://yterraillon.github.io");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MealPicker/1.0)");
            client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });
    }
}