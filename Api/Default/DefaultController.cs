using Application;

namespace Api.Default;

[Route("api/[controller]")]
[ApiController]
public class DefaultController(INotificationService notificationService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await notificationService.SendNotification("Hello from the API");
        return Ok(result);
    }
}