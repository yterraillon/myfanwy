namespace ComicGrabber.Application.Features.OglafDownloader;

public interface IOglafRssReader
{
    string GetLatestComicUrl(string url);
}