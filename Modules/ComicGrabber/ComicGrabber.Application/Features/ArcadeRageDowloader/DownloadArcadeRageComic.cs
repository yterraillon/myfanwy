namespace ComicGrabber.Application.Features.ArcadeRageDowloader;

public static class DownloadArcadeRageComic
{
    private const string FeedUrl = "https://arcaderage.co/feed/";
    private const string ComicName = "arcaderage";
    private const string Extension = ".png";
    
    public class Handler(IArcadeRageRssReader rssReader, IWebContentService webContentService, ComicDownloadService comicDownloadService) : IRequestHandler<Request, Response>
    {
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var latestComicDetails = await GetLatestComicDetails();
            await comicDownloadService.DownloadComic(ComicName, latestComicDetails.comicUrl, latestComicDetails.comicTitle, Extension);
            return new Response();
        }
        
        private async Task<(string comicUrl, string comicTitle)> GetLatestComicDetails()
        {
            var latestComicUrl = rssReader.GetLatestComicDetails(FeedUrl);
            var title = ComicDownloaderHelper.GetUrlLastSegment(latestComicUrl);
            
            var htmlContent = await webContentService.GetPageContent(latestComicUrl);
            var htmlDocument = ComicDownloaderHelper.ConvertStringToHtmlDocument(htmlContent); 
            return (ExtractComicUrl(htmlDocument), title);
        }
        
        private static string ExtractComicUrl(HtmlDocument htmlDocument)
        {
            //<img data-recalc-dims="1" fetchpriority="high" decoding="async" width="640" height="876" src="https://i0.wp.com/arcaderage.co/wp-content/uploads/2024/12/get-the-priest-mild-fever-mart-virkus.png?resize=640%2C876&amp;ssl=1" alt="" class="wp-image-1890" srcset="https://i0.wp.com/arcaderage.co/wp-content/uploads/2024/12/get-the-priest-mild-fever-mart-virkus.png?resize=748%2C1024&amp;ssl=1 748w, https://i0.wp.com/arcaderage.co/wp-content/uploads/2024/12/get-the-priest-mild-fever-mart-virkus.png?resize=219%2C300&amp;ssl=1 219w, https://i0.wp.com/arcaderage.co/wp-content/uploads/2024/12/get-the-priest-mild-fever-mart-virkus.png?resize=768%2C1051&amp;ssl=1 768w, https://i0.wp.com/arcaderage.co/wp-content/uploads/2024/12/get-the-priest-mild-fever-mart-virkus.png?resize=1122%2C1536&amp;ssl=1 1122w, https://i0.wp.com/arcaderage.co/wp-content/uploads/2024/12/get-the-priest-mild-fever-mart-virkus.png?resize=1496%2C2048&amp;ssl=1 1496w, https://i0.wp.com/arcaderage.co/wp-content/uploads/2024/12/get-the-priest-mild-fever-mart-virkus.png?w=1500&amp;ssl=1 1500w, https://i0.wp.com/arcaderage.co/wp-content/uploads/2024/12/get-the-priest-mild-fever-mart-virkus.png?w=1280&amp;ssl=1 1280w" sizes="(max-width: 640px) 100vw, 640px">
            var imgNode = htmlDocument.DocumentNode.SelectSingleNode("//img");
            var comicUrl = imgNode?.GetAttributeValue("src", string.Empty);
            return comicUrl ?? string.Empty;
        }
    }

    public record Request : IRequest<Response>;
    
    public record Response;
}