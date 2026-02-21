using ComicGrabber.Application.Features.OglafDownloader;
using Infrastructure.RssReader;

namespace ComicGrabber.Infrastructure.RssReaders;

public class OglafRssReaderService(IRssReader rssReader) : IOglafRssReader
{
    public string GetLatestComicUrl(string url)
    {
        var latestFeedItem = rssReader.GetLatestFeedItem(url);
        return latestFeedItem?.Links.First().Uri.ToString() ?? string.Empty;
    }
}