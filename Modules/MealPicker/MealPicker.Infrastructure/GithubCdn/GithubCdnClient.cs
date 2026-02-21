using Application.ObjectStorage;
using Infrastructure.HttpClients;
using MealPicker.Application.Models;

namespace MealPicker.Infrastructure.GithubCdn;

public class GithubCdnClient(HttpClient client) : IObjectStorageReader<List<Meal>>
{
    public async Task<List<Meal>> GetObjectContentAsync(string objectName)
    {
        var path = "/cdn/meal-picker/data/" + objectName;
        
        var result = await client.GetRequest<List<Meal>>(path);
        return result ?? [];
    }
}