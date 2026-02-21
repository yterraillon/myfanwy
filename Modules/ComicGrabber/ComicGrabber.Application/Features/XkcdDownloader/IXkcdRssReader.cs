namespace ComicGrabber.Application.Features.XkcdDownloader;

public interface IXkcdRssReader
{
    public string GetLatestComicSummary(string url);
}