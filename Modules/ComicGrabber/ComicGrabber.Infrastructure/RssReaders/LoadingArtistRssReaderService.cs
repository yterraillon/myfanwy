using ComicGrabber.Application.Features.LoadingArtistDownloader;
using Infrastructure.RssReader;

namespace ComicGrabber.Infrastructure.RssReaders;

public class LoadingArtistRssReaderService(IRssReader rssReader) : ILoadingArtistRssReader
{
    public string GetLatestComicUrl(string url)
    {
        var allFeedItems = rssReader.GetAllFeedItems(url);
        var latestComicFeedItem = allFeedItems.FirstOrDefault(item => item.Categories.First().Name == "comic");
        return latestComicFeedItem?.Links.First().Uri.ToString() ?? string.Empty;
    }
}