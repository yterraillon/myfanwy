namespace ComicGrabber.Application.Features;

public static class ComicDownloaderHelper
{
    public static HtmlDocument ConvertStringToHtmlDocument(string htmlContent)
    {
        var document = new HtmlDocument();
        document.LoadHtml(htmlContent);
        return document;
    }
        
    public static string GetUrlLastSegment(string url)
    {
        var uri = new Uri(url);
        var lastSegment = uri.Segments[^1].Trim('/');
        return lastSegment;
    }
}