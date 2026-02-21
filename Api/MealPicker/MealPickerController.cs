using MealPicker.Application.Features;

namespace Api.MealPicker;

[Route("api/[controller]")]
[ApiController]
public class MealPickerController(ISender mediator) : ControllerBase
{
    [HttpGet("meal-picker")]
    public async Task<IActionResult> GetMeals()
    {
        var request = new GetMeals.Request();
        
        var response = await mediator.Send(request);
        return Ok(response);
    }
}