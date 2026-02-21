using ComicGrabber.Application.Contracts;

namespace ComicGrabber.Infrastructure.LocalStorage;

public class LocalStorageService(ILocalStorageContext localStorageContext) : ILocalStorageService
{
   
    public void CreateLocalFile(string comicName, string title, byte[] content, string extension)
    {
        CreateFolder(comicName);
        
        var path = Path.Combine(localStorageContext.DownloadPath, comicName, $"{title}{extension}");
        File.WriteAllBytes(path, content);
    }
    
    private void CreateFolder(string comicName)
    {
        var path = Path.Combine(localStorageContext.DownloadPath, comicName);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}