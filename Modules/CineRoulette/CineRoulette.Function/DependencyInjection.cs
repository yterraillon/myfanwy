using Microsoft.Extensions.DependencyInjection;

namespace CineRoulette.Function;

public static class DependencyInjection
{
    public static void AddCineRouletteFunction(this IServiceCollection services)
    {
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<CineRoulette>());
        
        services.AddHttpClient<GetRandomMovie.CineRouletteN8nClient>("routine-generation-webhook", client =>
        {
            client.BaseAddress = new Uri("https://n8n.checquy.ovh/webhook/cineroulette");
        });
    }
}