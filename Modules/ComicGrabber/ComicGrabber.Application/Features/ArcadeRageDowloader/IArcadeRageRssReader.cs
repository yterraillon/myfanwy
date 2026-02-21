namespace ComicGrabber.Application.Features.ArcadeRageDowloader;

public interface IArcadeRageRssReader
{
    string GetLatestComicDetails(string url);
}