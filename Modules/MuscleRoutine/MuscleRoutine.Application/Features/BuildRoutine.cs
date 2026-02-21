using MuscleRoutine.Application.Contracts;

namespace MuscleRoutine.Application.Features;

public static class BuildRoutine
{
    public class Handler(IRoutineGenerationService routineGenerationService) : IRequestHandler<Request, Response>
    {
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var routine = await routineGenerationService.SendGenerationCommand(cancellationToken);
            return new Response(routine);
        }
    }
    
    public record Request() : IRequest<Response>;
    
    public record Response(List<Exercice> Routine);
}