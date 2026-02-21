namespace ComicGrabber.Application.Features.OglafDownloader;

public static class DownloadOglafComic
{
    private const string FeedUrl = "https://www.oglaf.com/feeds/rss/";
    private const string ComicName = "oglaf";
    private const string Extension = ".jpg";
    
    public class Handler(IOglafRssReader rssReader, IWebContentService webContentService, ComicDownloadService comicDownloadService) : IRequestHandler<Request, Response>
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
            //<img id="strip" src="https://media.oglaf.com/comic/sexy_pirate_adventures.jpg" alt="drawn on painkillers" title="Captain Mimic was later pardoned and became a breakfast cereal mascot.">
            var imgNode = htmlDocument.DocumentNode.SelectSingleNode("//img[@id='strip']");
            var comicUrl = imgNode?.GetAttributeValue("src", string.Empty);
            return comicUrl ?? string.Empty;
        }
    }   
    
    public record Request : IRequest<Response>;
    
    public record Response;
}