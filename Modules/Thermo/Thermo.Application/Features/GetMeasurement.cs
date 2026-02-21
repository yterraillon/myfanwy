using Application;

namespace Thermo.Application.Features;

public static class GetMeasurement
{
    public class Handler(IRepository<Measurement> measurementRepository) : IRequestHandler<Request, Response>
    {
        public Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var measurement = measurementRepository.Get(request.Id);

            return Task.FromResult(measurement is null ? 
                new Response(0, "none", 0, "none") : 
                new Response(measurement.Temperature, measurement.AirQuality, measurement.Humidity, measurement.Room));
        }
    }
    
    public class Request : IRequest<Response>
    {
        public Guid Id { get; init; }
    }
    
    public record Response(double Temperature, string AirQuality, double Humidity, string Room);
}