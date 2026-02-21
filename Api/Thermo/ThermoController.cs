using Thermo.Application.Features;

namespace Api.Thermo;

[Route("api/[controller]")]
[ApiController]
public class ThermoController(ISender mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateMeasurement(Command command)
    {
        var request = new CreateMeasurement.Request
        {
            Measurements = command.Measurements.Select(m => new CreateMeasurement.CreateMeasurementRequest
            {
                Temperature = m.Temperature,
                AirQuality = m.AirQuality,
                Humidity = m.Humidity,
                Room = m.Room
            }).ToList()
        };

        var response = await mediator.Send(request); 
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetMeasurement([FromQuery] string id)
    {
        var request = new GetMeasurement.Request
        {
            Id = new Guid(id)
        };
        var response = await mediator.Send(request);
        return Ok(response);
    }
    
    public class Command
    {
        public CreateMeasurementCommand[] Measurements { get; set; } = [];
    }
    
    public class CreateMeasurementCommand
    { // do not remove the Set, it breaks the model binding
        public string Temperature { get; set; } = string.Empty;
        public string AirQuality { get; set; } = string.Empty;
        public string Humidity { get; set; } = string.Empty;
        public string Room { get; set; } = string.Empty;
    }
}