namespace ComicGrabber.Application.Features.LoadingArtistDownloader;

public interface ILoadingArtistRssReader
{
    string GetLatestComicUrl(string url);
}