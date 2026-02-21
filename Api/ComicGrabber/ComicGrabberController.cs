using ComicGrabber.Application.Features.LoadingArtistDownloader;

namespace Api.ComicGrabber;

[Route("api/[controller]")]
[ApiController]
public class ComicGrabberController(ISender mediator) : ControllerBase
{
    [HttpGet("comics")]
    public async Task<IActionResult> GetComic()
    {
        var request = new DownloadLoadingArtistComic.Request();
        var response = await mediator.Send(request);

        return Ok();
    }
}