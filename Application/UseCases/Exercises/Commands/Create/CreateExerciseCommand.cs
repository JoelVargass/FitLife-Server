using ErrorOr;
using MediatR;

namespace Application.UseCases.Exercises.Commands.Create;

public record CreateExerciseCommand(
    string Name,
    string MuscleType,
    string Description,
    string Duration
    ) : IRequest<ErrorOr<Created>>;