using ComicGrabber.Application.Features;
using Microsoft.Extensions.DependencyInjection;

namespace ComicGrabber.Application;

public static class DependencyInjection
{
    public static void AddComicGrabberApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<ComicGrabber>());
        services.AddTransient<ComicDownloadService>();
    }
}