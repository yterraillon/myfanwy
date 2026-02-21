using System.Globalization;
using Application;
using Microsoft.Extensions.Logging;

namespace Thermo.Application.Features;

public static class CreateMeasurement
{
    public class Handler(IRepository<Measurement> measurementRepository, ILogger<CreateMeasurement.Handler> logger) : IRequestHandler<Request, Response>
    {
        public Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var measureTime = DateTime.Now;
            
            if (request.Measurements.Count < 4)
            {
                logger.LogError("{Timestamp} Not all measurements were provided - {MeasureTime}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), measureTime);
                return Task.FromResult(new Response([]));
            }
            
            var measurementIds = request.Measurements.Select(r => CreateMeasurement(r, measureTime)).ToList();
            return Task.FromResult(new Response(measurementIds.Select(m => m.ToString()).ToList()));
        }
        
        private Guid CreateMeasurement(CreateMeasurementRequest request, DateTime measureTime)
        {
            var temperature = ParseTemperature(request.Temperature);
            var humidity = ParseHumidity(request.Humidity);
            
            var measurement = Measurement.Create(temperature, request.AirQuality, humidity, request.Room, measureTime);
            if (measurement is not null)
            {
                measurementRepository.Add(measurement);
            }

            return measurement?.Id ?? Guid.Empty;
        }

        private static double ParseTemperature(string temperature)
        {
            var temperatureWithoutUnit = temperature.Replace(",",".").Replace("°C", string.Empty).Trim();
            var temperatureParsed = double.TryParse(temperatureWithoutUnit, NumberStyles.Any, CultureInfo.InvariantCulture, out var result) ? result : 0;
            
            return temperatureParsed;
        }
        
        private static double ParseHumidity(string humidity)
        {
            var humidityWithoutUnit = humidity.Replace(",",".").Replace("%", string.Empty).Trim();
            var humidityParsed = double.TryParse(humidityWithoutUnit, NumberStyles.Any, CultureInfo.InvariantCulture, out var result) ? result : 0;
            
            return humidityParsed;
        }
    }
    
    public class Request : IRequest<Response>
    {
        public List<CreateMeasurementRequest> Measurements { get; init; } = [];
    }
    
    public class CreateMeasurementRequest
    {
        public string Temperature { get; init; } = string.Empty;
        public string AirQuality { get; init; } = string.Empty;
        public string Humidity { get; init; } = string.Empty;
        public string Room { get; init; } = string.Empty;
    }   
    
    public record Response(List<string> MeasurementIds);
}