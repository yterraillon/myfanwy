using System.Net.Http.Headers;
using Application.ObjectStorage;
using Microsoft.Extensions.DependencyInjection;
using MuscleRoutine.Application.Contracts;
using MuscleRoutine.Application.Models;
using MuscleRoutine.Infrastructure.GithubCdn;
using MuscleRoutine.Infrastructure.HttpClients;

namespace MuscleRoutine.Infrastructure;

public static class DependencyInjection
{
    public static void AddMuscleRoutineInfrastructure(this IServiceCollection services)
    {
        services.AddHttpClient<IObjectStorageReader<List<Exercice>>, GithubCdnClient>("muscle-routine-client", client =>
        {
            client.BaseAddress = new Uri("https://yterraillon.github.io");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MuscleRoutine/1.0)");
            client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });
        
        services.AddHttpClient<IRoutineGenerationService, RoutineGenerationWebhookClient>("routine-generation-webhook", client =>
        {
            client.BaseAddress = new Uri("https://n8n.checquy.ovh/webhook/muscle-routine");
        });
    }
}