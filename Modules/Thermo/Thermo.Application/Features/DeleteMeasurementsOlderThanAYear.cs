using Application;
using Microsoft.Extensions.Logging;

namespace Thermo.Application.Features;

public static class DeleteMeasurementsOlderThanAYear
{
    public class Handler(IThermoReadRepository measurementRepository, IRepository<Measurement> writeRepository, ILogger<Handler> logger) : IRequestHandler<Request, Response>
    {
        public Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            const int daysInYear = 365;
            var aYearAgo = DateTime.Now.AddDays(-daysInYear);
            var oldMeasurementsIds = measurementRepository.GetAllMeasurementsBefore(aYearAgo).Select(m => m.Id).ToList();
            
            foreach (var id in oldMeasurementsIds)
            {
                writeRepository.Remove(id);
                logger.LogInformation("{Timestamp} Measurement with id {Id} from {AYearAgo} was removed", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), id, aYearAgo);
            }

            var response = new Response(oldMeasurementsIds.Select(m => m.ToString()).ToList());
            return Task.FromResult(response);
        }
    }

    public class Request : IRequest<Response>;
    
    public record Response(List<string> MeasurementIds);
}