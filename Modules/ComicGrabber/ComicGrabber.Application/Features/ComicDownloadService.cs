namespace ComicGrabber.Application.Features;

public class ComicDownloadService(IWebContentService webContentService, ILocalStorageService localStorageService, ILogger<ComicDownloadService> logger)
{
    public async Task DownloadComic(string comicName,string comicUrl, string comicTitle, string extension)
    {
        try
        {
            var imageContent = await webContentService.GetImageContent(comicUrl);
            localStorageService.CreateLocalFile(comicName, comicTitle, imageContent, extension);
            logger.LogInformation("{Timestamp} {ComicTitle} was saved successfully", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), comicTitle);
        }
        catch (Exception)
        {
            logger.LogWarning("{Date} {Comic} Could not save {ComicTitle}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), comicName, comicTitle);
            throw;
        }
    }
}