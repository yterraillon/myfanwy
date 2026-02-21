using ComicGrabber.Application.Contracts;

namespace ComicGrabber.Infrastructure.WebContent;

public class WebContentService(HttpClient client) : IWebContentService
{
    public async Task<string> GetPageContent(string url)
    {
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<byte[]> GetImageContent(string url)
    {
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsByteArrayAsync();
    }
}