using ComicGrabber.Application.Features.ArcadeRageDowloader;
using Infrastructure.RssReader;

namespace ComicGrabber.Infrastructure.RssReaders;

public class ArcadeRageRssReaderService(IRssReader rssReader) : IArcadeRageRssReader
{
    public string GetLatestComicDetails(string url)
    {
        var latestFeedItem = rssReader.GetLatestFeedItem(url);
        return latestFeedItem?.Links.First().Uri.ToString() ?? string.Empty;
    }
}