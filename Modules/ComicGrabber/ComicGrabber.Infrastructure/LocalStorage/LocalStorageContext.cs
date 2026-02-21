namespace ComicGrabber.Infrastructure.LocalStorage;

public class LocalStorageContext : ILocalStorageContext
{
    public string DownloadPath => @"..\Data\Comics";
}