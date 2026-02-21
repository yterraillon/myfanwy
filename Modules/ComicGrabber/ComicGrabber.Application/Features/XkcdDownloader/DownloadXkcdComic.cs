namespace ComicGrabber.Application.Features.XkcdDownloader;

public static class DownloadXkcdComic
{
    private const string FeedUrl = "https://xkcd.com/rss.xml";
    private const string ComicName = "xkcd";
    private const string Extension = ".png";
    
    public class Handler(IXkcdRssReader rssReader, ComicDownloadService comicDownloadService) : IRequestHandler<Request, Response>
    {
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var latestComicSummary = rssReader.GetLatestComicSummary(FeedUrl);
            var imageUrl = ExtractImageUrl(latestComicSummary);
            var imageTitle = GetFileNameWithoutExtension(imageUrl);
            await comicDownloadService.DownloadComic(ComicName, imageUrl, imageTitle, Extension);
            return new Response();
        }
        
        private static string ExtractImageUrl(string htmlContent)
        {
            var document = new HtmlDocument();
            document.LoadHtml(htmlContent);

            var imgNode = document.DocumentNode.SelectSingleNode("//img[@src]");
            var imageUrl = imgNode?.GetAttributeValue("src", string.Empty);

            return imageUrl ?? string.Empty;
        } 
        
        private static string GetFileNameWithoutExtension(string url)
        {
            var uri = new Uri(url);
            var fileName = Path.GetFileNameWithoutExtension(uri.LocalPath);
            return fileName;
        }
    }   
    
    public record Request : IRequest<Response>;
    
    public record Response;
}