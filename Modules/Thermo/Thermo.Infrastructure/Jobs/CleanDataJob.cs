using Quartz;
using MediatR;
using Thermo.Application.Features;

namespace Thermo.Infrastructure.Jobs;

public class CleanDataJob(ISender mediator) : IJob
{
    public static readonly JobKey Key = new ("clean-old-data", "data-cleaning");
    
    public async Task Execute(IJobExecutionContext context)
    {
        await Console.Out.WriteLineAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Thermo Clean Data Job is running.");

        var request = new DeleteMeasurementsOlderThanAYear.Request();
        var response = await mediator.Send(request);
        
        foreach (var id in response.MeasurementIds)
        {
            await Console.Out.WriteLineAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Deleted {id}");
        }
        await Console.Out.WriteLineAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Thermo Clean Data Job completed.");
    }
}