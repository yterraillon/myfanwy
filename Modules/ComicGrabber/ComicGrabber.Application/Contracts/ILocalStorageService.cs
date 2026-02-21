namespace ComicGrabber.Application.Contracts;

public interface ILocalStorageService
{
    void CreateLocalFile(string comicName, string title, byte[] content, string extension);
}