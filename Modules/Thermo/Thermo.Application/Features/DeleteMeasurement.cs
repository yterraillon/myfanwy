using Application;

namespace Thermo.Application.Features;

public static class DeleteMeasurement
{
    public class Handler(IRepository<Measurement> measurementRepository) : IRequestHandler<Request, Response>
    {
        public Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var success = measurementRepository.Remove(request.Id);
            return Task.FromResult(new Response(success));
        }
    }
    
    public class Request : IRequest<Response>
    {
        public Guid Id { get; init; }
    }
    
    public record Response(bool IsSuccess);
}