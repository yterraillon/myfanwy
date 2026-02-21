using ComicGrabber.Application.Features.ArcadeRageDowloader;
using ComicGrabber.Application.Features.LoadingArtistDownloader;
using ComicGrabber.Application.Features.OglafDownloader;
using MediatR;
using Quartz;

namespace ComicGrabber.Infrastructure.ScheduledJobs;

public class DownloadWeeklyComicJob(ISender mediator) : IJob
{
    public static readonly JobKey Key = new ("download-weekly", "comics");
    
    public async Task Execute(IJobExecutionContext context)
    {
        await Console.Out.WriteLineAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} DownloadWeeklyComic Job is running.");
        
        var oglafRequest = new DownloadOglafComic.Request();
        await mediator.Send(oglafRequest);
        await Console.Out.WriteLineAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Oglaf Comic downloaded");
        
        var arcadeRageRequest = new DownloadArcadeRageComic.Request();
        await mediator.Send(arcadeRageRequest);
        await Console.Out.WriteLineAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Arcade Rage Comic downloaded");
        
        var loadingArtistRequest = new DownloadLoadingArtistComic.Request();
        await mediator.Send(loadingArtistRequest);
        await Console.Out.WriteLineAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Loading Artist Comic downloaded");
    }
}