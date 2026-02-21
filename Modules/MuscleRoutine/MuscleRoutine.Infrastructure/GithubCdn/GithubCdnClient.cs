using Application.ObjectStorage;
using Infrastructure.HttpClients;
using MuscleRoutine.Application.Models;

namespace MuscleRoutine.Infrastructure.GithubCdn;

public class GithubCdnClient(HttpClient client) : IObjectStorageReader<List<Exercice>>
{
    public async Task<List<Exercice>> GetObjectContentAsync(string objectName)
    {
        var path = "/cdn/muscle-routine/data/" + objectName;
        
        var result = await client.GetRequest<List<Exercice>>(path);
        return result ?? [];
    }
}