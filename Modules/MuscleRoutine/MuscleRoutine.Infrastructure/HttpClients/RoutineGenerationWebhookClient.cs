using Infrastructure.HttpClients;
using MuscleRoutine.Application.Contracts;
using MuscleRoutine.Application.Models;

namespace MuscleRoutine.Infrastructure.HttpClients;

public class RoutineGenerationWebhookClient(HttpClient client) : IRoutineGenerationService
{
    public async Task<List<Exercice>> SendGenerationCommand(CancellationToken cancellationToken)
    {
        var result = await client.PostRequest<object, List<Exercice>>(client.BaseAddress!.ToString(), new {});
        return result?.ToList() ?? [];
    }
}