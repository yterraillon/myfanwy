using ComicGrabber.Application.Features.XkcdDownloader;
using Infrastructure.RssReader;

namespace ComicGrabber.Infrastructure.RssReaders;

public class XkcdRssReaderService(IRssReader rssReader) : IXkcdRssReader
{
    // Feed Item Description contains the image url
    public string GetLatestComicSummary(string url)
    {
        var latestFeedItem = rssReader.GetLatestFeedItem(url);
        return latestFeedItem?.Summary.Text ?? string.Empty;
    }
}