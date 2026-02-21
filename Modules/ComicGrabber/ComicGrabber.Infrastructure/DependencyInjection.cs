using ComicGrabber.Application.Contracts;
using ComicGrabber.Application.Features.ArcadeRageDowloader;
using ComicGrabber.Application.Features.LoadingArtistDownloader;
using ComicGrabber.Application.Features.OglafDownloader;
using ComicGrabber.Application.Features.XkcdDownloader;
using ComicGrabber.Infrastructure.LocalStorage;
using ComicGrabber.Infrastructure.RssReaders;
using ComicGrabber.Infrastructure.ScheduledJobs;
using ComicGrabber.Infrastructure.WebContent;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace ComicGrabber.Infrastructure;

public static class DependencyInjection
{
    public static void AddComicGrabberInfrastructure(this IServiceCollection services, bool isUsingDocker = false)
    {
        services.AddTransient<IOglafRssReader, OglafRssReaderService>();
        services.AddTransient<IXkcdRssReader, XkcdRssReaderService>();
        services.AddTransient<IArcadeRageRssReader, ArcadeRageRssReaderService>();
        services.AddTransient<ILoadingArtistRssReader, LoadingArtistRssReaderService>();
        
        services.AddHttpClient<IWebContentService, WebContentService>("webcomic-client");
        services.AddTransient<ILocalStorageService, LocalStorageService>();
        
        if (isUsingDocker)
        {
            services.AddSingleton<ILocalStorageContext, DockerLocalStorageContext>();
        }
        else
        {
            services.AddSingleton<ILocalStorageContext, LocalStorageContext>();
        }
        
        services.AddQuartz(q =>
        {
            q.AddJob<DownloadWeeklyComicJob>(opts => opts.WithIdentity(DownloadWeeklyComicJob.Key));
            q.AddTrigger(opts => opts
                .ForJob(DownloadWeeklyComicJob.Key)
                .WithIdentity("ComicGrabberSunday-trigger")
                .WithCronSchedule("0 0 20 ? * 7")); //Run every sunday at 20:00
            
            q.AddJob<DownloadDailyComicJob>(opts => opts.WithIdentity(DownloadDailyComicJob.Key));
            q.AddTrigger(opts => opts
                .ForJob(DownloadDailyComicJob.Key)
                .WithIdentity("ComicGrabberDaily-trigger")
                .WithCronSchedule("0 0 19 * * ?")); //Run every day at 17:00
        });
        
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    }
}