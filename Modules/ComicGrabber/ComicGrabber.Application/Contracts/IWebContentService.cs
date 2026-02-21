namespace ComicGrabber.Application.Contracts;

public interface IWebContentService
{
    Task<string> GetPageContent(string url);
    
    Task<byte[]> GetImageContent(string url);
}