using Application.ObjectStorage;

namespace MuscleRoutine.Application.Features;

public static class GetRoutine
{
    public class Handler(IObjectStorageReader<List<Exercice>> objectStorageReader) : IRequestHandler<Request, Response>
    {
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            const string routineFileName = "latest-routine.json";
            var routine = await objectStorageReader.GetObjectContentAsync(routineFileName);
            return new Response(routine);
        }
    }

    public record Request() : IRequest<Response>;
    
    public record Response(List<Exercice> Routine);
}