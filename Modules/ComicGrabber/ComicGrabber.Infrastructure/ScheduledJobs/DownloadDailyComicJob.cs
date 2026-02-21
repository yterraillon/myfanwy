using ComicGrabber.Application.Features.XkcdDownloader;
using MediatR;
using Quartz;

namespace ComicGrabber.Infrastructure.ScheduledJobs;

public class DownloadDailyComicJob(ISender mediator) : IJob
{
    public static readonly JobKey Key = new ("download-daily", "comics");
    
    public async Task Execute(IJobExecutionContext context)
    {
        await Console.Out.WriteLineAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} DownloadDailyComicJob Job is running.");
        
        var request = new DownloadXkcdComic.Request();
        await mediator.Send(request);
        await Console.Out.WriteLineAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Xkcd Comic downloaded");
    }
}