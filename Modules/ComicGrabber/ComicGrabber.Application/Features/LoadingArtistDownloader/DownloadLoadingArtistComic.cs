namespace ComicGrabber.Application.Features.LoadingArtistDownloader;

public static class DownloadLoadingArtistComic
{
    private const string FeedUrl = "https://loadingartist.com/index.xml";
    private const string ComicName = "loading-artist";
    private const string Extension = ".jpg";
    
    public class Handler(ILoadingArtistRssReader rssReader, IWebContentService webContentService, ComicDownloadService comicDownloadService) : IRequestHandler<Request, Response>
    {
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var latestComicDetails = await GetLatestComicDetails();
            await comicDownloadService.DownloadComic(ComicName, latestComicDetails.comicUrl, latestComicDetails.comicTitle, Extension);
            return new Response();
        }
        
        private async Task<(string comicUrl, string comicTitle)> GetLatestComicDetails()
        {
            var latestComicUrl = rssReader.GetLatestComicUrl(FeedUrl);
            var title = ComicDownloaderHelper.GetUrlLastSegment(latestComicUrl);
            
            var htmlContent = await webContentService.GetPageContent(latestComicUrl);
            var htmlDocument = ComicDownloaderHelper.ConvertStringToHtmlDocument(htmlContent); 
            return (ExtractComicUrl(htmlDocument), title);
        }
        
        private static string ExtractComicUrl(HtmlDocument htmlDocument)
        {
            //<picture><source type="image/webp" srcset="/comic/beach-bummed/beach-bummed_hu4686794506731560176.webp 550w, /comic/beach-bummed/beach-bummed_hu3001654285484539415.webp 800w" sizes="(max-width: 960px) 92vw, 550px"><img src="/comic/beach-bummed/beach-bummed_hu13189404519930143224.jpg" alt="Beach Bummed" title="Beach Bummed" width="800" height="3371" srcset="/comic/beach-bummed/beach-bummed_hu13189404519930143224.jpg 550w, /comic/beach-bummed/beach-bummed_hu4922036073882272850.jpg 800w" sizes="(max-width: 960px) 92vw, 550px"></picture>
            const string url = "https://loadingartist.com";
            
            var mainNode = htmlDocument.DocumentNode.SelectSingleNode("//div[contains(@class, 'main-image-container')]");
            var innerhtml = mainNode.InnerHtml;
            var picture = ComicDownloaderHelper.ConvertStringToHtmlDocument(innerhtml); 
            
            var imgNode = picture.DocumentNode.SelectSingleNode("//img");
            var comicPath = imgNode?.GetAttributeValue("src", string.Empty);

            return comicPath is null or "" ? string.Empty : $"{url}{comicPath}";
        }
        
    }

    public record Request : IRequest<Response>;
    
    public record Response;
}