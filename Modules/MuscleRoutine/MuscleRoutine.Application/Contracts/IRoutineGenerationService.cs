namespace MuscleRoutine.Application.Contracts;

public interface IRoutineGenerationService
{
    Task<List<Exercice>> SendGenerationCommand(CancellationToken cancellationToken);
}