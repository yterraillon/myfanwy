namespace ComicGrabber.Infrastructure.LocalStorage;

public class DockerLocalStorageContext : ILocalStorageContext
{
    public string DownloadPath => "/data/comics";
}