using MuscleRoutine.Application.Features;

namespace Api.MuscleRoutine;

[Route("api/[controller]")]
[ApiController]
public class MuscleRoutineController(ISender mediator) : ControllerBase
{
    [HttpGet("muscle-routine")]
    public async Task<IActionResult> GetMeals()
    {
        var request = new GetRoutine.Request();
        
        var response = await mediator.Send(request);
        return Ok(response);
    }
}